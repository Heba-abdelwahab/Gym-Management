using Microsoft.AspNetCore.Mvc;
using Shared.AuthDto;

namespace Services.Abstractions
{
    public interface IAuthService
    {
        Task<UserToReturnDto?> RegisterAsync(RegisterDto registerDto, string role);
        Task<UserToReturnDto?> LoginAsync(LoginDto loginDto);


        Task<ActionResult<bool>> CheckIfUserExist(string Email);

    }
}
