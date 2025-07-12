using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared.GymOwner;

namespace Presentation.Controllers
{
    [Authorize]
    public class GymOwnerController : ApiControllerBase
    {
        private IServiceManager _serviceManager;

        public GymOwnerController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        // [HttpGet("{id:int}/Gyms")]
        [HttpGet("Gyms")]
        public async Task<IActionResult> GetGymsForOwner()
        {
            var gyms = await _serviceManager.GymOwnerService.GetGymsForOwnerAsync();

            return Ok(gyms);
        }


        //[HttpGet("{id:int}/Info")]
        [HttpGet("Info")]
        public async Task<IActionResult> GetGymOwnerInfo()
        {
            var gymOwner = await _serviceManager.GymOwnerService.GetGymOwnerInfo();

            return Ok(gymOwner);
        }



        [HttpGet("{username}")]
        public async Task<ActionResult<GymOwnerInfoResultDto>> GetGymOwnerInfo(string username)
          => Ok(await _serviceManager.GymOwnerService.GetGymOwnerByUserNameAsync(username));

    }
}
