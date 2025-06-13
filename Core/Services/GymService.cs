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

        public GymService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
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
                gym.Media = ""; ///////////////////////////// ???
                unitOfWork.GetRepositories<Gym, int>().Insert(gym);
                await unitOfWork.CompleteSaveAsync();
            }

        }

        public async Task UpdateGym(int gymId,GymDto gymDto)
        {
            ////Console.WriteLine(x);

            //if (await validateGymFeatures(gymDto))
            //{

            //    //Gym gym = await unitOfWork.GetRepositories<Gym, int>().GetByIdWithSpecAsync(new GetGymWithFeaturesSpec(gymId));
            //    ////var deletedGymFeature = gym.GymFeatures
            //    //                      //  .Where(f => !gymDto.GymFeatures.Any(gf => gf.FeatureId == f.FeatureId)&& !gymDto.GymExtraFeatures.Any(gf => gf.FeatureId == f.FeatureId));
            //    //var deletedExGymFeature = gym.GymFeatures.Where(f => !gymDto.GymFeatures.Any(gf => gf.FeatureId == f.FeatureId) && f.Feature.IsExtra);

            //    ////// gym.GymFeatures.Where(gf => gf.Feature.IsExtra).ToList().ForEach(gf => gf.Feature = null);
            //    //// //gym.GymFeatures.Clear();
            //    //// await unitOfWork.CompleteSaveAsync();

            //    //gym = mapper.Map<Gym>(gymDto);
            //    //gym.Media = ""; ///////////////////////////// ???
            //    //unitOfWork.GetRepositories<Gym, int>().Update(gym);
            //    //await unitOfWork.CompleteSaveAsync();
            //}
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


    }
}
