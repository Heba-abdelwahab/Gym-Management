using Shared;
using Shared.Auth;
using Shared.GymOwner;
using Shared.TraineeGym;

namespace Services.Abstractions
{
    public interface IGymOwnerService
    {
        Task<IReadOnlyList<GymToReturnDto>> GetGymsForOwnerAsync();
        Task<GymOwnerToReturnDto> GetGymOwnerInfo();

        Task<GymOwnerInfoResultDto> GetGymOwnerByUserNameAsync(string username);

        Task<AuthAdminResultDto> CreateGymOwnerAsync(RegisterUserDto request);


    }
}
