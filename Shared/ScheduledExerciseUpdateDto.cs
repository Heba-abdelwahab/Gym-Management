using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class ScheduledExerciseUpdateDto
    {
        public int? Id { get; init; }
        public DateTime Day { get; init; }
        public int ExerciseId { get; set; }
    }
}
