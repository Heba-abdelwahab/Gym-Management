using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GymFeatureSpec : SpecificationBase<GymFeature, int>
    {
        public GymFeatureSpec(int gymId)
            : base(gymFeature => gymFeature.GymId == gymId)
        {
            AddIncludes(gymFeature => gymFeature.Feature);
        }

    }
}
