using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class GetNonExtraFeaturesSpec:SpecificationBase<Feature,int>
    {
        public GetNonExtraFeaturesSpec() :base(f=>!f.IsExtra){ 
        }
    }
}
