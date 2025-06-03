using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MuscleExerices:EntityBase<int>
    {
        public string Description {  get; set; }
        public string Media { get; set; }
        public DateTime Day { get; set; }
        public int MuscleId { get; set; }
        public int ExercisesScheduleId { get; set; }
        public ExercisesSchedule ExercisesSchedule { get; set; }
        public Muscle Muscle { get; set; }

    }
}
