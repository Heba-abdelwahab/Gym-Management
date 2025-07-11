using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class GetAllGymOwnersSpec:SpecificationBase<GymOwner, int>
    {
        public GetAllGymOwnersSpec(): base()
        {
            AddIncludes("AppUser");
            AddIncludes("Gyms.Trainees.Membership");
            AddIncludes("Gyms.Trainees.TraineeSelectedFeatures");
            AddIncludes("Gyms.Classes");
            AddIncludes("Gyms.Memberships");
        }
    }
}
