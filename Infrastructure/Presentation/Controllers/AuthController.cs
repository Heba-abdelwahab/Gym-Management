using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Services.Abstractions;
using Shared.AuthDto;
using System.Threading.Tasks;

namespace Presentation.Controllers
{

    public class AuthController : ApiBaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto, string? Role)
        {
            if (_authService.CheckIfUserExist(registerDto.Email).Result.Value)
                return BadRequest("User already exists");

            if (string.IsNullOrEmpty(Role))
                Role = "Trainee";
            var result = await _authService.RegisterAsync(registerDto, Role);

            if (result == null)
                return BadRequest("Registration failed. Please try again.");

            return Ok(result);
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            if (result == null)
                return Unauthorized();

            return Ok(result);
        }


    }
}
