using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public record ScheduledExerciseDto
    {
        [Required]
        public int ExerciseId { get; init; }

        [Required]
        public DateTime Day { get; init; }
       
    }
}
