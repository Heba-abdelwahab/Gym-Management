using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class GetGymWithFeaturesSpec:SpecificationBase<Gym,int>
    {
        public GetGymWithFeaturesSpec(int gymId):base(g=>g.Id==gymId)
        {
            AddIncludes($"{nameof(Gym.GymFeatures)}.{nameof(GymFeature.Feature)}");
            AddIncludes($"{nameof(Gym.GymFeatures)}.{nameof(GymFeature.Memberships)}");
            AddIncludes($"{nameof(Gym.GymFeatures)}.{nameof(GymFeature.TraineeSelectedFeatures)}");

        }
    }
}
