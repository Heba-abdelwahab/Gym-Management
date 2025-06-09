using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class MembershipSpec : SpecificationBase<Membership, int>
    {
        public MembershipSpec(int GymId):base(x => x.GymId == GymId)
        {
            AddIncludes("Features.Feature");
        }
    }
}
