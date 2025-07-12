using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record ExerciseScheduleResultDto
    {
        public int Id { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime EndDate { get; init; }
        public int CoachId { get; init; }
        public int TraineeId { get; init; }
        public ICollection<ScheduledExerciseResultDto> ScheduledExercises { get; init; } = new List<ScheduledExerciseResultDto>();
    }
}
