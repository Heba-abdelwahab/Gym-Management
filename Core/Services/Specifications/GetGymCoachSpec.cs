using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class GetGymCoachSpec:SpecificationBase<GymCoach,int>
    {
        public GetGymCoachSpec(int gymId,int coachId):
            base( gc => gc.GymId == gymId && gc.CoachId == coachId) 
        { 

        }
    }
}
