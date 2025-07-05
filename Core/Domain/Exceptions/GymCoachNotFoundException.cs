using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class GymCoachNotFoundException:Exception
    {
        public GymCoachNotFoundException(int GymId , int coachId) : base($"Not Found Coach with id:{coachId} with gym Id:{GymId}")
        {

        }
    }
}
