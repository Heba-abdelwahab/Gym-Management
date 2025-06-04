using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers;
using Services.Abstractions;
namespace Gymawy.Controllers;


public class UserController : ApiControllerBase
{
    private readonly IServiceManager _serviceManager;

    public UserController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    public async Task<IActionResult> GetUser()
    {
        var users = await _serviceManager.UserServices.GetCairoUser();
        return Ok(users);

    }

}
