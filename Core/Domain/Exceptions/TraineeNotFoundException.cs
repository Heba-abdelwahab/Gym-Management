using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class TraineeNotFoundException : NotFoundException
    {
        public TraineeNotFoundException(int id) : base($"Trainee with Id: {id} was Not Found.")
        {
        }
    }
    
}
