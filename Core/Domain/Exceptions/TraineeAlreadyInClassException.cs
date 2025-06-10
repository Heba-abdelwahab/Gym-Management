using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class TraineeAlreadyInClassException : BadRequestException
    {
        public TraineeAlreadyInClassException(int traineeId, int classId)
            : base($"Trainee with ID {traineeId} is already assigned to class with ID {classId}.")
        {
        }
    }
}
