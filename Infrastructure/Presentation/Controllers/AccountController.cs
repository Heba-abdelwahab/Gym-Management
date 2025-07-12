using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.Auth;

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


    [HttpPost("register/gymowner")]
    public async Task<ActionResult<AuthAdminResultDto>> RegisterGymOwner(RegisterUserDto registerGymOwner)
    => Ok(await _serviceManager.GymOwnerService.CreateGymOwnerAsync(registerGymOwner));



    //RegisterCoach
    [HttpPost("register/coach")]
    //public async Task<ActionResult<AuthCoachResultDto>> RegisterCoach(RegisterCoachDto registerCoach)
    public async Task<ActionResult<AuthCoachResultDto>> RegisterCoach([FromForm] RegisterCoachDto registerCoach)
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
    [HttpPost("login")]
    public async Task<ActionResult<AuthUserLoginResultDto>> RegisterTrainee(LoginUserDto login)
         => Ok(await _serviceManager.AuthenticationService.LoginUserAsync(login));


    [HttpGet("specializations")]
    public IActionResult GetSpecializations()
    {
        var list = Enum.GetValues(typeof(CoachSpecialization))
        .Cast<CoachSpecialization>()
        .Where(e => e != CoachSpecialization.None)
        .Select(e => new
        {
            name = e.ToString(),
            value = (int)e
        }).ToList();
        return Ok(list);
    }



}
