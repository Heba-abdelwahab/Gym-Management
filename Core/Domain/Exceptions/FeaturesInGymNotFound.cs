using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class FeaturesInGymNotFound : NotFoundException
    {
        public FeaturesInGymNotFound(int gemid) : base($"Gym With {gemid} Do Not Have Features")
        {
        }
    }
}