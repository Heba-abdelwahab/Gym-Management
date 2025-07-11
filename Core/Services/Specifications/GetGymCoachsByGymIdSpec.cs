using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class GetGymCoachsByGymIdSpec:SpecificationBase<GymCoach,int>
    {
        public GetGymCoachsByGymIdSpec(int gymId) : base(gc=>gc.GymId == gymId&& gc.Status == RequestStatus.Pending)
        {
            AddIncludes(gc => gc.WorkDays);
            AddIncludes($"{nameof(Coach)}.{nameof(AppUser)}");
        }
    }
}
