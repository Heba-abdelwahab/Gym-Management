using Shared.Auth;
using Shared.GymOwner;

namespace Services.Abstractions;

public interface IGymOwnerService
{

    Task<AuthAdminResultDto> CreateGymOwnerAsync(RegisterUserDto request);
    Task<GymOwnerInfoResultDto> GetGymOwnerByUserNameAsync(string username);
}
