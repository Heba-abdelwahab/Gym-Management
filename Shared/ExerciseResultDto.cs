using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record ExerciseResultDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Instructions { get; init; }
        public string? VideoUrl { get; init; }
        public string? ImageUrl { get; init; }
        public MuscleResultDto TargetMuscle { get; init; }
    }

}
