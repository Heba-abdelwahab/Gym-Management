using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class MemberShipNotFoundException:NotFoundException
    {
        public MemberShipNotFoundException(int memberid) : base($"MemberShip with Id:{memberid} Not Found")
        {

        }
    }
}
