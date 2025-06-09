using Shared;
using Shared.TraineeGym;

namespace Services.Abstractions;

public interface ITraineeService
{
    Task<AuthTraineeResultDto> CreateTraineeAsync(RegisterTraineeDto request);
    Task<List<TraineeToReturnDto>> GetTrineesByGem(int gymId);
    Task<bool> AssignCoachToTrainee(AssignCoachToTraineeDto assignCoachToTrainee);
    Task<IReadOnlyList<GymMembershipsDto>> GetAllMembershipsForGym(int GymId);
    Task<bool> AssignTraineeToMembership(int membershipId);
}
