using Shared;
using Shared.TraineeGym;
using System.Collections.Generic;

namespace Services.Abstractions;

public interface ITraineeService
{
    Task<AuthTraineeResultDto> CreateTraineeAsync(RegisterTraineeDto request);
    Task<List<TraineeToReturnDto>> GetTrineesByGem(int gymId);
    Task<bool> AssignCoachToTrainee(AssignCoachToTraineeDto assignCoachToTrainee);
    Task<IReadOnlyList<GymMembershipsDto>> GetAllMembershipsForGym(int GymId);
    Task<ClientSecretToReturnDto> AssignTraineeToMembership(int membershipId);

    Task<IReadOnlyList<Shared.TraineeGym.ClassTraineeToReturnDto>> GetClassesByGym(int GymId);
    Task<ClientSecretToReturnDto> JoinClass(int classId);

    Task<IReadOnlyList<GymFeatureToReturnDto>> GetGymFeatures(int gymId);
    /*Task<TraineeFeatureToReturnDto?>*/
    Task<ClientSecretToReturnDto> AssignTraineeToFeature(int featureId, int count);
    Task<TraineeSubscriptionsToReturnDto> TraineeSubscriptions();
    Task<IReadOnlyList<GymToReturnDto>> AllGyms();
    Task<GymToReturnDto> GetGymById(int gymId);
    Task<IReadOnlyList<ClassGymWithCoachToReturnDto>> GetAllClasses();
    Task<TraineeCoachToReturnDto> GetTraineeCoach();
}
