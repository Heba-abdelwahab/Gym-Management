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
    public class CoachController : ApiControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CoachController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost("request-gym")]
        public async Task<IActionResult> RequestToBecomeCoachAsync(int gymId, HashSet<WorkDayDto> workDayDtos)
        {
           
            var result = await _serviceManager.CoachService.RequestToBecomeCoachAsync(gymId, workDayDtos);

            if (result)
                return Ok("Request to become a coach has been successfully submitted.");
            
            else
                return StatusCode(500, "An error occurred while processing your request, Please try again.");
            
        }

        [HttpGet("PendingCoach/{gymId:int}")]
        public async Task<ActionResult<CoachPendingDto>> GetGymPendingCoachs(int gymId)
        {
            var coachs = await _serviceManager.CoachService.GetGymPendingCoachs(gymId);
            return Ok(coachs);
        }

        [HttpPost("HandleCoachJobRequest/{gymId:int}")]
        public async Task<ActionResult<CoachPendingDto>> HandleCoachJobRequest(int gymId,HandleJobRequestDto jobRequestDto)
        {
            var coachs = await _serviceManager.CoachService.GetGymPendingCoachs(gymId);
            return Ok(coachs);
        }
    }
}
