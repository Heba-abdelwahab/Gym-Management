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
        private readonly ICoachService _coachService;

        public CoachController(ICoachService coachService)
        {
            _coachService = coachService;
        }

        [HttpPost("request-gym")]
        public async Task<IActionResult> RequestToBecomeCoachAsync(int gymId, HashSet<WorkDayDto> workDayDtos)
        {
           
            var result = await _coachService.RequestToBecomeCoachAsync(gymId, workDayDtos);

            if (result)
                return Ok("Request to become a coach has been successfully submitted.");
            
            else
                return StatusCode(500, "An error occurred while processing your request, Please try again.");
            
        }
    }
}
