using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ScheduledExercise:EntityBase<int>
    {
     
        public DateTime Day { get; set; }
        public int ExercisesScheduleId { get; set; }
        public ExercisesSchedule? ExercisesSchedule { get; set; }
        public int ExerciseId { get; set; }
        public Exercise Exercise { get; set; }
    }
}
