using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record MealResultDto
    {
        public int Id { get; init; }
        [Required]
        public string Description { get; init; }
        [Required]
        public DateTime Day { get; init; }
        [Required]
        public MealType MealType { get; init; }
    }
}
