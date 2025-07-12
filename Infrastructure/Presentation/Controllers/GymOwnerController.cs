using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class GymOwnerController : ApiControllerBase
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
        [HttpGet("GymOwnerData/{GymOwnerid:int}")]
        public async Task<IActionResult> GetAllDataForGymOwner(int GymOwnerid)
        {
            var gymOwnerData = await _serviceManager.GymOwnerService.GetAllDataForGymOwner(GymOwnerid);
            return Ok(gymOwnerData);
        }

        [HttpGet("GymMemberships/{id:int}")]
        public async Task<IActionResult> GetGymOwnerMemberships(int id)
        {
            var memberships = await _serviceManager.GymOwnerService.GetGymownerMemberships(id);
            return Ok(memberships);
        }
    }

}