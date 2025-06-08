using Microsoft.AspNetCore.Authorization;
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
    public async Task<ActionResult<AuthAdminResultDto>> RegisterAdmin(RegisterUserDto registerAdmin)
     => Ok(await _serviceManager.AdminService.CreateAdminAsync(registerAdmin));


    //RegisterCoach
    [HttpPost("register/coach")]
    public async Task<ActionResult<AuthCoachResultDto>> RegisterCoach(RegisterCoachDto registerCoach)
    => Ok(await _serviceManager.CoachService.CreateCoachAsync(registerCoach));



    //RegisterTrainee 
    [HttpPost("register/trainee")]
    public async Task<ActionResult<AuthTraineeResultDto>> RegisterTrainee(RegisterTraineeDto registerTrainee)
    => Ok(await _serviceManager.TraineeService.CreateTraineeAsync(registerTrainee));


    #region Testing Id
    [HttpGet("userId")]
    [Authorize]
    public ActionResult GetID()
    {

        return Ok(_serviceManager.UserServices.Id);
    }
    #endregion




    //login 

}
