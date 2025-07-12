using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class ExerciseScheduleNotFoundException : NotFoundException
    {
        public ExerciseScheduleNotFoundException(int scheduleId) : base($"ExerciseSchedule with ID {scheduleId} not found.")
        {
        }
    }
}
