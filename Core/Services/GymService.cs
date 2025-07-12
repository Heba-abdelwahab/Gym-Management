using AutoMapper;
using AutoMapper.Execution;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Shared.Cloudinary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using Domain.Entities.Domain.Entities;

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

        public async Task<bool> DeleteGymMemberShip(int membershipID)
        {

            var membership = await unitOfWork.GetRepositories<Membership, int>().GetByIdWithSpecAsync(new GetMemberShipSpec(membershipID));
            if (membership == null) throw  new MembershipNotFoundException(membershipID);
             unitOfWork.GetRepositories<Membership,int>().Delete(membership);
            var result=await unitOfWork.CompleteSaveAsync();
            if (result)
            {
                return true;
            }
            return false;
        }

        public async Task<List<GymFeatureReturnDto>> GetGymFeatures(int GymId)
        {
            var Features =await unitOfWork.GetRepositories<GymFeature, int>()
                .GetAllWithSpecAsync(new GetGymFeatureSpec(GymId));
            if (!Features.Any())
            {
                throw new FeaturesInGymNotFound(GymId);
            }
            var result= mapper.Map<List<GymFeatureReturnDto>>(Features);

            return result;
        }

        public async Task<MemberShipDto> GetGymMemberShipById(int MemberiId)
        {

            var MemberShip =await unitOfWork.GetRepositories<Membership, int>().GetByIdWithSpecAsync(new GetMemberShipSpec(MemberiId));
            if (MemberShip == null)
            {
                throw new MembershipNotFoundException(MemberiId);
            }

            var Mapping = mapper.Map<MemberShipDto>(MemberShip);

            return Mapping;

        }

        public async Task<List<MemberShipDto>> GetGymMemberShips(int GymID)
        {
            var memerships =await unitOfWork.GetRepositories<Membership, int>()
                .GetAllWithSpecAsync(new GetMemberShipsByGymDtoSpec(GymID));
          var  mappingList=mapper.Map <List<MemberShipDto>> (memerships);
            if(!mappingList.Any())
                throw new MemberShipInGymNotFoundException(GymID);

            else
                return mappingList;
        }

        public async Task<bool> GymMemberShip(MemberShipReturnDto memberShip)
        {
           
            var MapingMemberShip=mapper.Map<Membership>(memberShip);
         
            

            foreach (var featureId in memberShip.GymFeaturesId)
            {
                var gymfeature = await unitOfWork.GetRepositories<GymFeature,int>().GetByIdWithSpecAsync(new GymFeatureSpec(featureId));
                if (gymfeature != null)
                {
                    MapingMemberShip.Features.Add(gymfeature);
                }
            }

            unitOfWork.GetRepositories<Membership, int>().Insert(MapingMemberShip);
      var result= await  unitOfWork.CompleteSaveAsync();
            if (result) return true;
            return false;

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
        public async Task<GymGetDto> GetGymWithFeaturesById(int gymId)
        {
            Gym gym = await unitOfWork.GetRepositories<Gym, int>().GetByIdWithSpecAsync(new GetGymWithImagesFeaturesSpec(gymId));

            if (gym == null)
                throw new GymNotFoundException(gymId);

            var gymWithFeaturesDto = mapper.Map<GymGetDto>(gym, opt => opt.Items["CloudinaryBaseUrl"] = config.CurrentValue.CloudinaryBaseUrl);
            gymWithFeaturesDto.MediaUrl = config.CurrentValue.CloudinaryBaseUrl + gymWithFeaturesDto.MediaUrl;
            gymWithFeaturesDto.GymImagesUrl = gymWithFeaturesDto.GymImagesUrl.Select(img => config.CurrentValue.CloudinaryBaseUrl + img).ToList();
            return gymWithFeaturesDto;
            
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
                        Type = MediaType.Image
                    }
                });
            }

            gym.Media = new MediaValueObj
            {
                Url = logoUploadResult.ImageName,
                PublicId = logoUploadResult.PublicId,
                Type = MediaType.Image
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
                    Type = MediaType.Image
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
                            Type = MediaType.Image
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
                Type = MediaType.Image,
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
                Type = MediaType.Image,
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

       public async Task<IEnumerable<PendingGymDto>> GetPendingGyms()
       {
            var pendingGyms = await unitOfWork.GetRepositories<Gym,int>().GetAllWithSpecAsync(new GetPendingGymsSpec());
            var pendingGymsDtos = mapper.Map<IEnumerable<PendingGymDto>>(pendingGyms);
            return pendingGymsDtos;
       }
        public async Task<int> HandleGymAddRequest(int gymId,bool IsAccepted)
        {
            Gym gym = await unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
            if (gym == null)
                throw new GymNotFoundException(gymId);

            gym.AddGymStatus= IsAccepted ? RequestStatus.Accepted : RequestStatus.Rejected;
            if (!await unitOfWork.CompleteSaveAsync())
                throw new Exception($"fail to update Gym Add Request of gym id {gymId}");
            return gymId;
        }

        public async Task<bool> UpdateGymMemberShip(int MemberID, MemberShipReturnDto memberShip)
        {
           
            var MapingMemberShip =await unitOfWork.GetRepositories<Membership,int>()
                .GetByIdWithSpecAsync(new GetMemberShipSpec(MemberID));
      

            MapingMemberShip.Name= memberShip.Name;
            MapingMemberShip.Cost = memberShip.Cost;
            MapingMemberShip.Description = memberShip.Description;
            MapingMemberShip.Duration = memberShip.Duration;
            MapingMemberShip.Count= memberShip.Count;
           MapingMemberShip.Features.Clear();

            foreach (var featureId in memberShip.GymFeaturesId)
            {
                var gymfeature = await unitOfWork.GetRepositories<GymFeature, int>().
                    GetByIdWithSpecAsync(new GymFeatureSpec(featureId));
                if (gymfeature != null)
                {
                    MapingMemberShip.Features.Add(gymfeature);
                }
            }

            unitOfWork.GetRepositories<Membership, int>().Update(MapingMemberShip);
            var result = await unitOfWork.CompleteSaveAsync();
            if (result) return true;
            return false;
        }
    }
}
