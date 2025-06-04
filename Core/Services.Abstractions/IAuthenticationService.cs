using Shared;

namespace Services.Abstractions;

public interface IAuthenticationService
{
    public Task<UserResultDto> RegisterUserAsync(RegisterUserDto registerModel);
    public Task<UserResultDto> LoginUserAsync(LoginUserDto loginModel);
    public Task<bool> CheckEmailExist(string email);

}
