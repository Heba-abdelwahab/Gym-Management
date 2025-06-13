using Domain.Entities;
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
        public DateTime Day { get; init; }
        public ExerciseResultDto Exercise { get; init; }
    }
}
