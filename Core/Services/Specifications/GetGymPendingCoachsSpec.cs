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
    public class GetGymPendingCoachsSpec:SpecificationBase<Coach,int>
    {
        public GetGymPendingCoachsSpec(int gymId) : base(
            coach=>coach.GymCoaches.
                   Any(gc=>gc.GymId== gymId && gc.Status == RequestStatus.Pending)
        )
        {
            AddIncludes(c => c.AppUser);
        }
    }
}
