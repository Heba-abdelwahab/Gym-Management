using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Shared.Cloudinary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Services
{
    public class GymService: IGymService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userServices;
        private readonly IPhotoService photoService;
        private readonly IOptionsMonitor<CloudinarySettings> config;

        public GymService(IUnitOfWork unitOfWork , IMapper mapper, IUserService _userServices,IPhotoService photoService, IOptionsMonitor<CloudinarySettings> _config)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this._userServices = _userServices;
            this.photoService = photoService;
            config = _config;
        }

        public async Task<GymGetDto> GetGymById(int gymId)
        {
            Gym gym = await unitOfWork.GetRepositories<Gym, int>().GetByIdWithSpecAsync(new GetGymWithImagesSpec(gymId));

            if (gym == null)
                throw new GymNotFoundException(gymId);
            
            var gymDeto = mapper.Map<GymGetDto>(gym);
            gymDeto.MediaUrl= config.CurrentValue.CloudinaryBaseUrl+gymDeto.MediaUrl;
            gymDeto.GymImagesUrl=gymDeto.GymImagesUrl.Select(img=> config.CurrentValue.CloudinaryBaseUrl + img).ToList();
            return gymDeto;

        }
        private async Task<Boolean> validateGymFeatures(GymDto gymDto)
        {
            List<string> errors = new List<string>();
            var allFeatures = await unitOfWork.GetRepositories<Feature, int>().GetAllAsync();

            if (gymDto.GymExtraFeatures == null && gymDto.GymFeatures == null)
            {
                errors.Add($"must add Features");
                throw new ValidationException(errors);
            }

            foreach (var gymfeature in gymDto.GymFeatures)
                if (!allFeatures.Any(f => f.Id == gymfeature.FeatureId))
                    errors.Add($"there is no feature  with id {gymfeature.FeatureId} in system ");

            if (errors.Count > 0)
                throw new ValidationException(errors);

            return true;
        }
        //public async Task RequestAddGym (GymWithFilesDto gymWithFilesDto, GymDto gymDto)
        //{

        //   if( await validateGymFeatures(gymDto))
        //   {
        //        IEnumerable<Task<PhotoUploadedResult>> loadingExFeaturesImageTask = null;
        //        IEnumerable<Task<PhotoUploadedResult>> loadingFeaturesImageTask = null;
        //        var loadingImagesTasks = gymWithFilesDto.GymImages.Select(img=>photoService.AddPhotoAsync(img)).ToList();
        //        var logoLoadingTask = photoService.AddPhotoAsync(gymWithFilesDto.Media);

        //        if (gymWithFilesDto.GymExtraFeaturesImages != null)
        //            loadingExFeaturesImageTask = gymWithFilesDto.GymExtraFeaturesImages.Select(img => photoService.AddPhotoAsync(img)).ToList();

        //        if (gymWithFilesDto.GymFeaturesImages != null)
        //            loadingFeaturesImageTask = gymWithFilesDto.GymFeaturesImages.Select(img => photoService.AddPhotoAsync(img)).ToList();


        //        //gym.GymOwnerId = _userServices.Id.Value;
        //       // gym.GymOwnerId = 1;

        //        IEnumerable<PhotoUploadedResult> photoUploadedResults=null, ExFeaturesImageResult=null, FeaturesImageResult=null;

        //        photoUploadedResults = await Task.WhenAll(loadingImagesTasks);

        //        if (loadingExFeaturesImageTask != null)
        //            ExFeaturesImageResult = await Task.WhenAll(loadingExFeaturesImageTask);

        //        if (loadingFeaturesImageTask != null)
        //            FeaturesImageResult = await Task.WhenAll(loadingFeaturesImageTask);

        //        var LogoUploadedResults = await logoLoadingTask;


        //        Gym gym = mapper.Map<Gym>(gymDto, opt =>
        //        {
        //            opt.Items["exFeatureImages"] = ExFeaturesImageResult;
        //            opt.Items["FeatureImages"] = FeaturesImageResult;

        //        });

        //        foreach (var image in photoUploadedResults)
        //            gym.Images.Add(new Media()
        //            {
        //                MediaValue = new MediaValueObj()
        //                {
        //                    Url = image.ImageName,
        //                    PublicId = image.PublicId,
        //                    Type = MediaType.Img
        //                }

        //            });

        //         gym.Media =new MediaValueObj()
        //         {
        //             Url = LogoUploadedResults.ImageName,
        //             PublicId = LogoUploadedResults.PublicId,
        //             Type = MediaType.Img
        //         };


        //         unitOfWork.GetRepositories<Gym, int>().Insert(gym);
        //         if( ! await unitOfWork.CompleteSaveAsync() )
        //            throw new Exception ("fail to request add Gym");

        //    }

        //}


        public async Task RequestAddGym(GymWithFilesDto gymWithFilesDto, GymDto gymDto)
        {
            if (!await validateGymFeatures(gymDto))
                throw new Exception("Gym features validation failed.");

            var uploadLogoTask = photoService.AddPhotoAsync(gymWithFilesDto.Media);
            var uploadMainImagesTasks = gymWithFilesDto.GymImages.Select(img => photoService.AddPhotoAsync(img)).ToList();
            var uploadExtraFeaturesTasks = gymWithFilesDto.GymExtraFeaturesImages?.Select(img => photoService.AddPhotoAsync(img)).ToList();
            var uploadFeaturesTasks = gymWithFilesDto.GymFeaturesImages?.Select(img => photoService.AddPhotoAsync(img)).ToList();

            var mainImagesResult = await Task.WhenAll(uploadMainImagesTasks);
            var logoUploadResult = await uploadLogoTask;
            var extraFeaturesResult = uploadExtraFeaturesTasks != null ? await Task.WhenAll(uploadExtraFeaturesTasks) : null;
            var featuresResult = uploadFeaturesTasks != null ? await Task.WhenAll(uploadFeaturesTasks) : null;

            var gym = mapper.Map<Gym>(gymDto, opt =>
            {
                opt.Items["exFeatureImages"] = extraFeaturesResult;
                opt.Items["FeatureImages"] = featuresResult;
            });

            foreach (var image in mainImagesResult)
            {
                gym.Images.Add(new Media
                {
                    MediaValue = new MediaValueObj
                    {
                        Url = image.ImageName,
                        PublicId = image.PublicId,
                        Type = MediaType.Img
                    }
                });
            }

            gym.Media = new MediaValueObj
            {
                Url = logoUploadResult.ImageName,
                PublicId = logoUploadResult.PublicId,
                Type = MediaType.Img
            };

            unitOfWork.GetRepositories<Gym, int>().Insert(gym);

            if (!await unitOfWork.CompleteSaveAsync())
                throw new Exception("Failed to request add Gym.");
        }

        public async Task UpdateGym(int gymId, GymWithFilesUpdate gymWithFilesUpdate, GymUpdateDto gymUpdateDto)
        {
            Gym gym;

            if (gymWithFilesUpdate.GymImages==null)
                gym = await unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
            else
                gym = await unitOfWork.GetRepositories<Gym, int>().GetByIdWithSpecAsync(new GetGymWithImagesSpec(gymId));

            if (gym == null)
                throw new GymNotFoundException(gymId);

            mapper.Map(gymUpdateDto, gym);

            var uploadLogoTask = gymWithFilesUpdate.Media != null ? photoService.AddPhotoAsync(gymWithFilesUpdate.Media) : null;
            var uploadImagesTasks = gymWithFilesUpdate.GymImages != null ? gymWithFilesUpdate.GymImages.Select(img => photoService.AddPhotoAsync(img)) : null;

            var LogoPhotoRes = uploadLogoTask==null ? null : await uploadLogoTask;
            var ImagesPhotoRes = uploadImagesTasks == null ? null : await Task.WhenAll(uploadImagesTasks);


            if (LogoPhotoRes != null)
            {             
                bool isdeleted= await photoService.DeletePhotoAsync(gym.Media.PublicId);
                gym.Media = new MediaValueObj()
                {
                    Url = LogoPhotoRes.ImageName,
                    PublicId = LogoPhotoRes.PublicId,
                    Type = MediaType.Img
                };
            }


            if (ImagesPhotoRes != null)
            {
                var deleteImageTaskes = gym.Images.Select(img => photoService.DeletePhotoAsync(img.MediaValue.PublicId));
                await Task.WhenAll(deleteImageTaskes);

                foreach (var image in gym.Images)
                    unitOfWork.GetRepositories<Media, int>().Delete(image);
                gym.Images.Clear();

                foreach (var image in ImagesPhotoRes)
                    gym.Images.Add(new Media()
                    {
                        MediaValue = new MediaValueObj()
                        {
                            Url = image.ImageName,
                            PublicId = image.PublicId,
                            Type = MediaType.Img
                        }

                    });
            }

            unitOfWork.GetRepositories<Gym,int>().Update(gym);

            if (!await unitOfWork.CompleteSaveAsync())
                throw new Exception($"fail to update Gym with id {gymId}");
        }

        public IEnumerable<ItemDto> GetGymTypes()
        {
            return Enum.GetValues(typeof(GymType))
                   .Cast<GymType>()
                   .Select(gt=>new ItemDto( gt.ToString(),  (int)gt) )
                   .ToList();
        }
        public async Task<IEnumerable<ItemDto>> GetGymFeatures()
        {
            var features = await unitOfWork.GetRepositories<Feature,int>().GetAllWithSpecAsync(new GetNonExtraFeaturesSpec());
            return mapper.Map<IEnumerable< ItemDto>>(features);
        }

        public async Task<IEnumerable<GymFeatureDto>> GetFeaturesByGymId(int gymId)
        {
            Gym gym = await unitOfWork.GetRepositories<Gym,int>().GetByIdAsync(gymId);
            if (gym == null)
                throw new GymNotFoundException(gymId);
            
            var gymFeatures = await unitOfWork.GetRepositories<GymFeature, int>().GetAllWithSpecAsync(new GymFeatureSpec(gymId));
            var gymFeaturesDto= mapper.Map<IEnumerable<GymFeatureDto>>(gymFeatures, opt => opt.Items["CloudinaryBaseUrl"]= config.CurrentValue.CloudinaryBaseUrl);

            return gymFeaturesDto;
        }

        public async Task<GymFeatureDto> GetGymFeatureById(int gymFeatureId)
        {
            var gymFeature = await unitOfWork.GetRepositories<GymFeature, int>().GetByIdWithSpecAsync(new WholeFeature(gymFeatureId));
            if(gymFeature == null)
                throw new GymFeatureNotFoundException(gymFeatureId);
            var gymFeatureDto = mapper.Map<GymFeatureDto>(gymFeature);
            return gymFeatureDto;
        }

        public async Task<GymFeatureDto> AddNonExGymFeature(int gymId, NonExGymFeatureDto NonExGymFeatureDto)
        {
            Gym gym = await unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
            Feature feature = await unitOfWork.GetRepositories<Feature, int>().GetByIdAsync(NonExGymFeatureDto.FeatureId);

            if (gym == null)
                throw new GymNotFoundException(gymId);
            if (feature == null)
                throw new FeatureNotFoundExceptions(NonExGymFeatureDto.FeatureId);

            var newGymFeature = mapper.Map<GymFeature>(NonExGymFeatureDto);
            newGymFeature.GymId = gymId;

            var photoResult = await photoService.AddPhotoAsync(NonExGymFeatureDto.Image);
            newGymFeature.Image = new MediaValueObj()
            {
                PublicId = photoResult.PublicId,
                Url = photoResult.ImageName,
                Type = MediaType.Img,
            };


            unitOfWork.GetRepositories<GymFeature, int>().Insert(newGymFeature);
            await unitOfWork.CompleteSaveAsync();

            return mapper.Map<GymFeatureDto>(newGymFeature, opt => opt.Items["CloudinaryBaseUrl"] = config.CurrentValue.CloudinaryBaseUrl);
        }
        public async Task<GymFeatureDto> AddExtraGymFeature(int gymId, ExGymFeatureDto gymFeatureDto)
        {
            Gym gym = await unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
            if (gym == null)
                throw new GymNotFoundException(gymId);

            var newGymFeature = mapper.Map<GymFeature>(gymFeatureDto);
            newGymFeature.GymId = gymId;

            var photoResult = await photoService.AddPhotoAsync(gymFeatureDto.Image);
            newGymFeature.Image = new MediaValueObj()
            {
                PublicId = photoResult.PublicId,
                Url = photoResult.ImageName,
                Type = MediaType.Img,
            };

            unitOfWork.GetRepositories<GymFeature, int>().Insert(newGymFeature);
            await unitOfWork.CompleteSaveAsync();

            return mapper.Map<GymFeatureDto>(newGymFeature, opt => opt.Items["CloudinaryBaseUrl"] = config.CurrentValue.CloudinaryBaseUrl);

        }
        public async Task<GymFeatureDto> UpdateGymFeature(int gymFeatureId, GymFeaturePutDto GymFeaturePutDto)
        {
            GymFeature gymFeature = await unitOfWork.GetRepositories<GymFeature, int>().GetByIdAsync(gymFeatureId);
            if(gymFeature == null)
                throw new GymFeatureNotFoundException(gymFeatureId);

            //if (gymFeature.Cost != GymFeaturePutDto.Cost) ;
            //***notify memebers about cost change of feature***

            await photoService.DeletePhotoAsync(gymFeature.Image.PublicId);
            var photoResult=await photoService.AddPhotoAsync(GymFeaturePutDto.Image);

            mapper.Map(GymFeaturePutDto, gymFeature);
            gymFeature.Image.PublicId = photoResult.PublicId;
            gymFeature.Image.Url = photoResult.ImageName;

            unitOfWork.GetRepositories<GymFeature,int>().Update(gymFeature);

            if (!await unitOfWork.CompleteSaveAsync())
                throw new Exception($"fail to update Gym Feature of id {gymFeatureId}");

            return mapper.Map<GymFeatureDto>(gymFeature, opt => opt.Items["CloudinaryBaseUrl"] = config.CurrentValue.CloudinaryBaseUrl);

        }

        public async Task DeleteGymFeature(int gymFeatureId)
        {
            var gymFeature = await unitOfWork.GetRepositories<GymFeature, int>().GetByIdWithSpecAsync(new WholeFeature(gymFeatureId));
            if (gymFeature == null)
                throw new GymFeatureNotFoundException(gymFeatureId);

            // ***notify members who use this feature***
            // ***notify members who subsribe to program contain that feature***

            await photoService.DeletePhotoAsync(gymFeature.Image.PublicId);
 

            unitOfWork.GetRepositories<GymFeature, int>().Delete(gymFeature);
            if (gymFeature.Feature.IsExtra)
                unitOfWork.GetRepositories<Feature, int>().Delete(gymFeature.Feature);

            if (!await unitOfWork.CompleteSaveAsync())
                throw new Exception($"fail to delete Gym Feature of id {gymFeatureId}");
        }


    }
}
