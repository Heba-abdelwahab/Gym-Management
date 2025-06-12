using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record ExerciseScheduleUpdateDto
    {
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public ICollection<MuscleExerciseResultDto> MuscleExerices { get; init; } = new List<MuscleExerciseResultDto>();
    }
}
