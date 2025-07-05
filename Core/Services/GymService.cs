using AutoMapper;
using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Exceptions;
using Newtonsoft.Json.Linq;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Services
{
    public class GymService: IGymService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUserService _userServices;
        public GymService(IUnitOfWork unitOfWork , IMapper mapper, IUserService _userServices)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this._userServices = _userServices;
        }

        public async Task<GymGetDto> GetGymById(int gymId)
        {
            Gym gym = await unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);

            if (gym == null)
                throw new GymNotFoundException(gymId);
            
            var gymDeto = mapper.Map<GymGetDto>(gym);

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
        public async Task RequestAddGym (GymDto gymDto)
        {

           if( await validateGymFeatures(gymDto))
            {
                Gym gym = mapper.Map<Gym>(gymDto);

                gym.GymOwnerId = _userServices.Id.Value;
                gym.Media = ""; ///////////////////////////// ???
                unitOfWork.GetRepositories<Gym, int>().Insert(gym);
                if( ! await unitOfWork.CompleteSaveAsync() )
                    throw new Exception ("fail to request add Gym");
            }

        }

        public async Task UpdateGym(int gymId, GymUpdateDto gymUpdateDto)
        {
            Gym gym = await unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);

            if (gym == null)
                throw new GymNotFoundException(gymId);

            mapper.Map(gymUpdateDto, gym);
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
            var gymFeaturesDto= mapper.Map<IEnumerable< GymFeatureDto>>(gymFeatures);

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

        public async Task AddNonExGymFeature(int gymId, NonExGymFeatureDto NonExGymFeatureDto)
        {
            Gym gym = await unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
            Feature feature = await unitOfWork.GetRepositories<Feature, int>().GetByIdAsync(NonExGymFeatureDto.FeatureId);

            if (gym == null)
                throw new GymNotFoundException(gymId);
            if (feature == null)
                throw new FeatureNotFoundExceptions(NonExGymFeatureDto.FeatureId);

            var newGymFeature = mapper.Map<GymFeature>(NonExGymFeatureDto);
            newGymFeature.GymId = gymId;

            unitOfWork.GetRepositories<GymFeature, int>().Insert(newGymFeature);
            await unitOfWork.CompleteSaveAsync();
        }
        public async Task AddExtraGymFeature(int gymId, ExGymFeatureDto gymFeatureDto)
        {
            Gym gym = await unitOfWork.GetRepositories<Gym, int>().GetByIdAsync(gymId);
            if (gym == null)
                throw new GymNotFoundException(gymId);

            var newGymFeature = mapper.Map<GymFeature>(gymFeatureDto);
            newGymFeature.GymId = gymId;

            unitOfWork.GetRepositories<GymFeature, int>().Insert(newGymFeature);
            await unitOfWork.CompleteSaveAsync();
        }
        public async Task UpdateGymFeature(int gymFeatureId, GymFeaturePutDto GymFeaturePutDto)
        {
            GymFeature gymFeature = await unitOfWork.GetRepositories<GymFeature, int>().GetByIdAsync(gymFeatureId);
            if(gymFeature == null)
                throw new GymFeatureNotFoundException(gymFeatureId);

            //if (gymFeature.Cost != GymFeaturePutDto.Cost) ;
            //***notify memebers about cost change of feature***

            mapper.Map(GymFeaturePutDto, gymFeature);
            unitOfWork.GetRepositories<GymFeature,int>().Update(gymFeature);

            if (!await unitOfWork.CompleteSaveAsync())
                throw new Exception($"fail to update Gym Feature of id {gymFeatureId}");

        }

        public async Task DeleteGymFeature(int gymFeatureId)
        {
            GymFeature gymFeature = await unitOfWork.GetRepositories<GymFeature, int>().GetByIdAsync(gymFeatureId);
            if (gymFeature == null)
                throw new GymFeatureNotFoundException(gymFeatureId);

            // ***notify members who use this feature***
            // ***notify members who subsribe to program contain that feature***

            unitOfWork.GetRepositories<GymFeature, int>().Delete(gymFeature);

            if (!await unitOfWork.CompleteSaveAsync())
                throw new Exception($"fail to delete Gym Feature of id {gymFeatureId}");
        }


    }
}
