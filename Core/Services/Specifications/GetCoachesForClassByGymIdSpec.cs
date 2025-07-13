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
    public class GetCoachesForClassByGymIdSpec:SpecificationBase<GymCoach,int>
    {
        public GetCoachesForClassByGymIdSpec(int gymId):base(g=>g.GymId==gymId && g.Status==RequestStatus.Accepted)
        {
            AddIncludes("Coach.AppUser");
        }
    }
}
