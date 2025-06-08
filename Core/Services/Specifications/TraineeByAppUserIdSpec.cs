using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class TraineeByAppUserIdSpec:SpecificationBase<Trainee,int>
    {
        public TraineeByAppUserIdSpec(string appUserId):base(t=>t.AppUserId==appUserId)
        {
            
        }
    }
}
