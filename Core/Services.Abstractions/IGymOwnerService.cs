using Shared;
using Shared.TraineeGym;

namespace Services.Abstractions
{
    public interface IGymOwnerService
    {
        Task<IReadOnlyList<GymToReturnDto>> GetGymsForOwnerAsync(int ownerId);
    }
}
