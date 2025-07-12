using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class GymOwnerController: ApiControllerBase
    {
        private IServiceManager _serviceManager;

        public GymOwnerController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet("{id:int}/Gyms")]
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
    }
}
