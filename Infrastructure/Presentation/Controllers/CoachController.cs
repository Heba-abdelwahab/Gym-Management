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

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCoachesBygem(int id)
        {

            var Coaches=await _serviceManager.CoachService.GetCoachesbyGym(id);
            if (Coaches.Any())
                return Ok(Coaches);

            else
                return StatusCode(500, "An error occurred, Please try again.");

        }


        }
    }
