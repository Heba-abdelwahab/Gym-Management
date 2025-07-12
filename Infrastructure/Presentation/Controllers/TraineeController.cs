using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.TraineeGym;

using Shared.Trainee;


namespace Presentation.Controllers;

[Authorize]
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
        var trainees = await _serviceManager.TraineeService.GetTrineesByGem(gymId);
        if (trainees.Any())
            return Ok(trainees);

        else
            return StatusCode(500, "An error occurred when Trying to get Trainees, Please try again.");


    }



    [HttpPost("AssignCoachToTrainee")]
    public async Task<IActionResult> AssignCoachToTrainee(AssignCoachToTraineeDto assignCoachToTrainee)
    {
     var t= await _serviceManager.TraineeService.AssignCoachToTrainee(assignCoachToTrainee);
        if(t)
        return Ok(new {message= "success" });
        else
            return BadRequest(new { message="An error occurred when Assigning coach to trainee, Please try again." });


    }

    [AllowAnonymous]
    [HttpGet("get-memberships/{id:int}")]
    public async Task<IActionResult> GetAllMembershipsForGym(int id)
    {
        var memberships = await _serviceManager.TraineeService.GetAllMembershipsForGym(id);
        if (memberships is not null)
            return Ok(memberships);
        else
            return StatusCode(500, "An error occurred when trying to get memberships, Please try again.");
    }

    [HttpPost("assign-membership/{membershipId:int}")]
    public async Task<IActionResult> AssignTraineeToMembership(int membershipId)
    {
        var result = await _serviceManager.TraineeService.AssignTraineeToMembership(membershipId);
        if (result is not null)
            return Ok(result);
        else
            return StatusCode(500, "An error occurred when trying to assign trainee to membership OR you have this active membership before, Please try again.");
    }

    // Classes For Trainee
    [AllowAnonymous]
    [HttpGet("classes/{gymId:int}")]
    public async Task<IActionResult> GetClassesByGym(int gymId)
    {
        var classes = await _serviceManager.TraineeService.GetClassesByGym(gymId);
        if (classes is not null)
            return Ok(classes);
        else
            return StatusCode(500, "An error occurred when trying to get classes, Please try again.");
    }

    // Join Class
    [HttpPost("join-class/{classId:int}")]
    public async Task<IActionResult> JoinClass(int classId)
    {
        var result = await _serviceManager.TraineeService.JoinClass(classId);
        if (result is not null)
            return Ok(result);
        else
            return StatusCode(500, "An error occurred when trying to join class, Please try again.");
    }

    // Show Gym Features
    [HttpGet("features/{gymId:int}")]
    [AllowAnonymous]

    public async Task<IActionResult> GetGymFeatures(int gymId)
    {
        var features = await _serviceManager.TraineeService.GetGymFeatures(gymId);
        if (features is not null)
            return Ok(features);
        else
            return StatusCode(500, "An error occurred when trying to get gym features, Please try again.");
    }

    // Assign Trainee To Feature
    [HttpPost("add-feature/{featureId:int}")]
    public async Task<IActionResult> AssignTraineeToFeature(int featureId, [FromQuery] int count)
    {
        var result = await _serviceManager.TraineeService.AssignTraineeToFeature(featureId, count);
        if (result is not null)
            return Ok(result);
        else
            return StatusCode(500, "An error occurred when trying to assign trainee to feature, Please try again.");
    }


    // ================================== Trainee Subscriptions ===================================
    [HttpGet("subscriptions")]
    public async Task<IActionResult> GetTraineeSubscriptions()
    {
        var subscriptions = await _serviceManager.TraineeService.TraineeSubscriptions();
        if (subscriptions is not null)
            return Ok(subscriptions);
        else
            return StatusCode(500, "An error occurred when trying to get subscriptions, Please try again.");
    }


    // =================================== Trainee Gym ==============================================
    [HttpGet("all-gyms")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllGymsData()
    {
        var GymsData = await _serviceManager.TraineeService.AllGyms();
        if (GymsData is not null)
            return Ok(GymsData);
        else
            return StatusCode(500, "An error occurred when trying to get Gyms, Please try again.");
    }

    [AllowAnonymous]
    [HttpGet("gym/{gymId:int}")]
    public async Task<IActionResult> GetGymById(int gymId)
    {
        var gym = await _serviceManager.TraineeService.GetGymById(gymId);
        if (gym is not null)
            return Ok(gym);
        else
            return StatusCode(500, "An error occurred when trying to get gym, Please try again.");
    }

    // =========================== get all classes ===========================
    [AllowAnonymous]
    [HttpGet("classes")]
    public async Task<IActionResult> GetAllClasses()
    {
        var classes = await _serviceManager.TraineeService.GetAllClasses();
        if (classes is not null)
            return Ok(classes);
        else
            return StatusCode(500, "An error occurred when trying to get classes, Please try again.");
    }



    //get trainee's coach
    [HttpGet("coach")]
    public async Task<IActionResult> GetTraineeCoach()
    {
        var coach = await _serviceManager.TraineeService.GetTraineeCoach();
        if (coach is not null)
            return Ok(coach);
        else
            return StatusCode(500, "An error occurred when trying to get trainee's coach, Please try again.");
    }

    // ================================= Update Trainee Profile ===================================
    [HttpPut("update-profile")]
    public async Task<IActionResult> UpdateTraineeProfile([FromForm] EditTraineeProfileDto editTraineeProfileDto)
    {
        var result = await _serviceManager.TraineeService.EditTraineeProfile(editTraineeProfileDto);
        if (result is not null)
            return Ok(result);
        else
            return StatusCode(500, "An error occurred when trying to update trainee profile, Please try again.");
    }




    [HttpGet("{username}")]
    public async Task<ActionResult<TraineeInfoResultDto>> GetTraineeInfo(string username)
    => Ok(await _serviceManager.TraineeService.GetTraineeByUserName(username));



    [HttpGet("diet")]
    public async Task<ActionResult<IReadOnlyList<MealScheduleResultDto>>> GetDiet()
    {
        var diet = await _serviceManager.TraineeService.GetDietAsync();
        if (diet is not null)
            return Ok(diet);
        else
            return StatusCode(500, "An error occurred when trying to get diet, Please try again.");
    }

    [HttpGet("exercise-schedule")]
    public async Task<ActionResult<IReadOnlyList<ExerciseScheduleResultDto>>> GetExerciseSchedule()
    {
        var exerciseSchedule = await _serviceManager.TraineeService.GetExerciseScheduleAsync();
        if (exerciseSchedule is not null)
            return Ok(exerciseSchedule);
        else
            return StatusCode(500, "An error occurred when trying to get exercise schedule, Please try again.");
    }
}
