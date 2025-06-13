using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    public class GymController:ApiControllerBase
    {
        private readonly IServiceManager serviceManager;

        public GymController(IServiceManager serviceManager)
        {
            this.serviceManager = serviceManager;
        }

        [HttpPost("RequestAddGym")]
        public async Task<ActionResult> RequestAddGym(GymDto gymDto)
        {
            await serviceManager.GymService.RequestAddGym(gymDto);
            return Ok();
        }

        [HttpPost("UpdateGym/{gymId:int}")]
        public async Task<ActionResult> UpdateGym(int gymId, GymDto gymDto)
        {
            await serviceManager.GymService.UpdateGym(gymId, gymDto);
            return Ok();
        }

        [HttpGet("GetGymTypes")]
        public ActionResult GetGymTypes()
        {
            var gymTypes = serviceManager.GymService.GetGymTypes();
            return Ok(gymTypes);
        }

        [HttpGet("GetGymFeatures")]
        public async Task< ActionResult> GetGymFeatures()
        {
            var features = await serviceManager.GymService.GetGymFeatures();
            return Ok(features);
        }
    }
}
