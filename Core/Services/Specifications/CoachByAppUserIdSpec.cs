using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class CoachByAppUserIdSpec:SpecificationBase<Coach,int>
    {
        public CoachByAppUserIdSpec(string appUserId):base(c=>c.AppUserId== appUserId)
        {
            
        }
    }
}
