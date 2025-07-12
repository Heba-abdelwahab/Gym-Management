using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class GetGymWithImagesFeaturesSpec:SpecificationBase<Gym,int>
    {
        public GetGymWithImagesFeaturesSpec(int gymId):base(g=>g.Id==gymId)
        {
            AddIncludes($"{ nameof(Gym.GymFeatures)}.{nameof(GymFeature.Feature)}");
            AddIncludes(g => g.Images);
        }
    }
}
