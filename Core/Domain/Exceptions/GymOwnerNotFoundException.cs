using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class GymOwnerNotFoundException: NotFoundException
    {
        public GymOwnerNotFoundException(int id): base($"Gym Owner with Id: {id} Not Found!")
        { }
    }
}
