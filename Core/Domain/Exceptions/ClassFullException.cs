using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ClassFullException : BadRequestException
    {
        public ClassFullException(int classId)
            : base($"The class with ID {classId} is full and cannot accept more trainees.")
        {
        }
    }
}
