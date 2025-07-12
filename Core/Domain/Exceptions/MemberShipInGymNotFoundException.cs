using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class MemberShipInGymNotFoundException : NotFoundException
    {
        public MemberShipInGymNotFoundException(int gemid) : base($"Gym With {gemid} Do Not Have Memberships")
        {
        }
    }
}
