using Shared;

namespace Services.Abstractions;

public interface ICoachService
{
    // Request into gym to become a coach
    Task<bool> RequestToBecomeCoachAsync(int gymId, HashSet<WorkDayDto> workDays);
    Task<List<CoachToReturnDto>> GetCoachesbyGym(int gymId);
}
