using Shared;

namespace Services.Abstractions;

public interface IAuthenticationService
{
    public Task<AuthUserResultDto> RegisterUserAsync(RegisterUserDto registerModel);
    public Task<AuthUserLoginResultDto> LoginUserAsync(LoginUserDto loginModel);
    public Task<bool> CheckEmailExist(string email);

}
