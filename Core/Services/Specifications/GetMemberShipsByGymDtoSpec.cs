using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GetMemberShipsByGymDtoSpec:SpecificationBase<Membership,int>
    {
      public  GetMemberShipsByGymDtoSpec(int GymID):base(m=>m.GymId==GymID)
        {
            AddIncludes("Features.Feature");
        }
    }
}
