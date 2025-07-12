using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GetMemberShipSpec:SpecificationBase<Membership,int>
    {

        public GetMemberShipSpec(int MId):base(m=>m.Id==MId)
        {

            AddIncludes("Features.Feature");
        }
    }
}
