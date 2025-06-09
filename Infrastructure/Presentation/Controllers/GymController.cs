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


        [HttpPost("requestAddGym")]
        public async Task<ActionResult> RequestAddGym(GymDto gymDto)
        {
            await serviceManager.GymService.RequestAddGym(gymDto);
            return Ok();
        }
    }
}
