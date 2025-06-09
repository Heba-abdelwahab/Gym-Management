using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentation.Controllers;

public class TraineeController : ApiControllerBase
{


    private readonly IServiceManager _serviceManager;

    public TraineeController(IServiceManager serviceManager)
    {

        _serviceManager = serviceManager;
    }

    [HttpGet("Trainees/{gymId:int}")]
    public async Task<IActionResult> GetTraineeByGYm(int gymId)
    {
        var trainees=await _serviceManager.TraineeService.GetTrineesByGem(gymId);
        if(trainees.Any())
        return Ok(trainees);

        else
            return StatusCode(500, "An error occurred when Trying to get Trainees, Please try again.");


    }



    [HttpPost]
    public async Task<IActionResult> AssignCoachToTrainee(AssignCoachToTraineeDto assignCoachToTrainee)
    {
     var t= await _serviceManager.TraineeService.AssignCoachToTrainee(assignCoachToTrainee);
        if(t)
        return Ok("success");
        else
            return StatusCode(500, "An error occurred when Assigning coach to trainee, Please try again.");


    }


}
