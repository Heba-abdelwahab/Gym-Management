using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public record MuscleExerciseDto
    {
        [Required]
        public string Description { get; init; }

        public string Media { get; init; } = string.Empty;

        [Required]
        public DateTime Day { get; init; }

        [Required]
        public int MuscleId { get; init; } 
    }
}
