using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class GymFeatureNotFoundException : NotFoundException
    {
        public GymFeatureNotFoundException(int  gemid) : base($"Gym With {gemid} Do Not Have Features")
        {
        }
    }
}
