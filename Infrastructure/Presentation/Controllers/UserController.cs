using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abstractions;
namespace Gymawy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserService userService{ get; set; }
        public UserController(IUserService userService)
        {
           this.userService = userService;
        }
        public async Task<IActionResult> GetUser()
        {
            var users = await userService.GetCairoUser();
            return Ok(users);

        }
        
    }
}
