using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GymCoachesSpec:SpecificationBase<GymCoach,int>
    {
        public GymCoachesSpec(int coachid):base(g=>g.CoachId==coachid)
        {
        }
    }
}
