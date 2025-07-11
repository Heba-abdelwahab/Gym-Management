using Shared.Auth;

namespace Services.Abstractions;

public interface IAdminService
{

    Task<AuthAdminResultDto> CreateAdminAsync(RegisterUserDto request);


}
