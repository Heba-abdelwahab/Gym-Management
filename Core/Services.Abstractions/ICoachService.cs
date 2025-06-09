using Shared;

namespace Services.Abstractions;

public interface ICoachService
{
    // Request into gym to become a coach
    Task<bool> RequestToBecomeCoachAsync(CoachRequestGymDto coachRequest);
    Task<List<CoachToReturnDto>> GetCoachesbyGym(int gymId);

    // Creates a new meal schedule for a trainee.
    Task<bool> CreateDietAsync(int traineeId, MealScheduleDto dietDto);


    Task<AuthCoachResultDto> CreateCoachAsync(RegisterCoachDto request);



    Task<IEnumerable<CoachPendingDto>> GetGymPendingCoachs(int gymId);
    Task HandleCoachJobRequest(int gymId, HandleJobRequestDto jobRequestDto);
}
