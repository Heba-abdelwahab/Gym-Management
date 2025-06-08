using Shared;

namespace Services.Abstractions;

public interface ICoachService
{
    // Request into gym to become a coach
    Task<bool> RequestToBecomeCoachAsync(int gymId, HashSet<WorkDayDto> workDays);

    // Creates a new meal schedule for a trainee.
    Task<bool> CreateDietAsync(int traineeId, MealScheduleDto dietDto);


    Task<AuthCoachResultDto> CreateCoachAsync(RegisterCoachDto request);



}
