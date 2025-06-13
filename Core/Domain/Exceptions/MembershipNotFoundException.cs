using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class MembershipNotFoundException : NotFoundException
    {
        public MembershipNotFoundException(int membershipId)
            : base($"Membership with ID {membershipId} not found.")
        {
        }
    }
}
