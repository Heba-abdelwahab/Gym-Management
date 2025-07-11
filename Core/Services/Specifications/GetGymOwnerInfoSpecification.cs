using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class GetGymOwnerInfoSpecification:SpecificationBase<GymOwner, int>
    {
        public GetGymOwnerInfoSpecification(int id): base(o => o.Id == id)
        {
            AddIncludes("AppUser");
        }
    }
}
