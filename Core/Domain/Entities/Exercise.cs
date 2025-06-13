using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Exercise : EntityBase<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Instructions { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }

        public int TargetMuscleId { get; set; }

        public Muscle TargetMuscle { get; set; }

        public ICollection<ScheduledExercise>? ScheduledExercises { get; set; } = new List<ScheduledExercise>();
    }
}
