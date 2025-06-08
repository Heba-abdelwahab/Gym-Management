using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shared;

public record MealDto
{
    [Required]
    public string Description { get; init; }

    [Required]
    public DateTime Day { get; init; }

    [Required]
    public MealType MealType { get; init; }
}

public record MealScheduleDto
{
    [Required]
    public DateTime StartDate { get; init; }

    [Required]
    public DateTime EndDate { get; init; }

    [Required]
    [MinLength(3)]
    public ICollection<MealDto> Meals { get; init; } = new HashSet<MealDto>();
}
