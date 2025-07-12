using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class WholeFeature : SpecificationBase<GymFeature, int>
    {
        public WholeFeature(int FeatureId) : base(x => x.Id == FeatureId)
        {
            AddIncludes(x => x.Feature);

        }
    }
}