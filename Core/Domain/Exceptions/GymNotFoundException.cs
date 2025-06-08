using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class GymNotFoundException : NotFoundException
    {
        public GymNotFoundException(int GymId):base($"Gym with Id:{GymId} Not Found")
        {
            
        }
    }
}
