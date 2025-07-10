using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record ExerciseScheduleDto
    {
        [Required]
        public DateTime StartDate { get; init; }

        [Required]
        public DateTime EndDate { get; init; }

        [Required]
        [MinLength(1)]
        public ICollection<ScheduledExerciseDto> ScheduledExercises { get; init; } = new List<ScheduledExerciseDto>();
    }
}
