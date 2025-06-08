using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class GymOwnerByAppUserIdSpec:SpecificationBase<GymOwner,int>
    {
        public GymOwnerByAppUserIdSpec(string AppUserId):base(go=>go.AppUserId==AppUserId)
        {
            
        }
    }
}
