using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class GymFeatureNotFoundException : NotFoundException
    {
        public GymFeatureNotFoundException(int gymFeatureId) : base($"Gym feature with ID {gymFeatureId} not found.")
        {
        }
    }
}
