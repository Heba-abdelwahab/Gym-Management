using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class TraineeAlreadyHasActiveMembershipException : BadRequestException
    {
        public TraineeAlreadyHasActiveMembershipException(int TraineeId)
            : base($"Trainee with id {TraineeId} already has an active membership.")
        {
        }
    }
}
