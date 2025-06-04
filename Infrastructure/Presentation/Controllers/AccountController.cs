using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentation.Controllers;

public class AccountController : ApiControllerBase
{
    private readonly IServiceManager _serviceManager;

    public AccountController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }


    //RegisterAdmin

    [HttpPost("register/admin")]
    public async Task<ActionResult<AdminResultDto>> RegisterAdmin(RegisterUserDto registerUser)
     => Ok(await _serviceManager.AdminService.CreateAdminAsync(registerUser));


    //RegisterCoach
    //[HttpPost("register/coach")]
    //public async Task<ActionResult<AdminResultDto>> RegisterAdmin(RegisterUserDto registerUser)
    //=> Ok(await _serviceManager.AdminService.CreateAdminAsync(registerUser));


    //RegisterTrainee 


    //login 

}
