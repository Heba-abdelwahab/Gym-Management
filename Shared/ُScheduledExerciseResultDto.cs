using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record ScheduledExerciseResultDto
    {
        public int Id { get; init; }
        public string Description { get; init; }
        public string? Media { get; init; }
        public DateTime Day { get; init; }
        public int MuscleId { get; init; }
    }
}
