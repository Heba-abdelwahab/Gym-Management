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
    public class GetCoachDashboardSpec : SpecificationBase<Coach, int>
    {
        public GetCoachDashboardSpec(int coachId) : base(c => c.Id == coachId)
        {
            AddIncludes(c => c.GymCoaches);

            AddIncludes("GymCoaches.Gym");

            // Include the Classes and their related Gym for the GymName

            AddIncludes("Classes");

            // Include the Trainees and their related AppUser (for name) and Membership
            AddIncludes("Trainees.AppUser");
        }
    }
}
