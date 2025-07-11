using Shared.Auth;

namespace Services.Abstractions;

public interface IGymOwnerService
{

    Task<AuthAdminResultDto> CreateGymOwnerAsync(RegisterUserDto request);
}
