using Shared;
using Shared.TraineeGym;

namespace Services.Abstractions
{
    public interface IGymOwnerService
    {
        Task<IReadOnlyList<GymToReturnDto>> GetGymsForOwnerAsync(int ownerId);
        Task<List<GymOwnerDataDto>> GetAllDataForGymOwner(int ownerId);
        Task<List<OwnerMembershipdto>> GetGymownerMemberships(int ownerId);
        Task<GymOwnerToReturnDto> GetGymOwnerInfo(int ownerId);
    }
}
