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
        [HttpGet("/Gyms")]
        public async Task<IActionResult> GetGymsForOwner(int id)
        {
            var gyms = await _serviceManager.GymOwnerService.GetGymsForOwnerAsync(id);

            return Ok(gyms);
        }


        [HttpGet("{id:int}/Info")]
        public async Task<IActionResult> GetGymOwnerInfo(int id)
        {
            var gymOwner = await _serviceManager.GymOwnerService.GetGymOwnerInfo(id);

            return Ok(gymOwner);
        }



        [HttpGet("{username}")]
        public async Task<ActionResult<GymOwnerInfoResultDto>> GetGymOwnerInfo(string username)
          => Ok(await _serviceManager.GymOwnerService.GetGymOwnerByUserNameAsync(username));

    }
}
