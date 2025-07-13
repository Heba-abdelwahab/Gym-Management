using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GymOwnerDataSpec:SpecificationBase<GymOwner, int>
    {
        public GymOwnerDataSpec(int id) : base(o => o.Id == id)
        {
            AddIncludes(o => o.Gyms);
            AddIncludes("Gyms.Memberships");
            AddIncludes("Gyms.Classes");
            AddIncludes("Gyms.Trainees");
            AddIncludes("Gyms.GymCoaches");
            AddIncludes("Gyms.GymFeatures");
            
        }
    }
}
