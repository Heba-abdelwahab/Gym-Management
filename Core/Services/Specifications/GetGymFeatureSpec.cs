using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GetGymFeatureSpec:SpecificationBase<GymFeature,int>
    {
        public GetGymFeatureSpec(int GymID) :base(GF=>GF.GymId==GymID)
        {
            AddIncludes(g => g.Feature);
        }
    }
}
