using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class CoeachesNotFoundException : NotFoundException
    {
        public CoeachesNotFoundException(int coachid) : base($"Coach With {coachid} Not Found")
        {
        }
    }
}
