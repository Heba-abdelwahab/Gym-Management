using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using Shared.coach;

namespace Presentation.Controllers;

//[Authorize]
public class CoachController : ApiControllerBase
{
    private readonly IServiceManager _serviceManager;

    public CoachController(IServiceManager serviceManager)
    {

        _serviceManager = serviceManager;
    }

    [HttpPost("request-gym")]
    public async Task<IActionResult> RequestToBecomeCoachAsync(CoachRequestGymDto request)
    {

        var result = await _serviceManager.CoachService.RequestToBecomeCoachAsync(request);

        if (result)
            return Ok("Request to become a coach has been successfully submitted.");

        else
            return StatusCode(500, "An error occurred while processing your request, Please try again.");

    }

    [HttpGet("GymPendingCoach/{gymId:int}")]
    public async Task<ActionResult<CoachPendingDto>> GetGymPendingCoachs(int gymId)
    {
        var coachs = await _serviceManager.CoachService.GetGymPendingCoachs(gymId);
        return Ok(coachs);
    }

    [HttpPost("HandleCoachJobRequest/{gymId:int}")]
    public async Task<ActionResult> HandleCoachJobRequest(int gymId, HandleJobRequestDto jobRequestDto)
    {
        await _serviceManager.CoachService.HandleCoachJobRequest(gymId, jobRequestDto);
        return Ok(jobRequestDto.CoachId);
    }

    #region Diet for Trainee
    [HttpPost("diet/{traineeId:int}")]
    public async Task<IActionResult> CreateDietForTrainee(int traineeId, MealScheduleDto dietDto)
    {
        var result = await _serviceManager.CoachService.CreateDietAsync(traineeId, dietDto);

        if (result)
        {
            return StatusCode(201, "Diet created successfully.");
        }

        return StatusCode(500, "An error occurred while processing your request, Please try again.");
    }

    [HttpGet("diet/{dietId}")]
    public async Task<IActionResult> GetDietById(int dietId)
    {
        var diet = await _serviceManager.CoachService.GetDietByIdAsync(dietId);
        return diet is null ? NotFound() : Ok(diet);
    }

    [HttpGet("diets/{traineeId}")]
    public async Task<IActionResult> GetAllDietsForTrainee(int traineeId)
    {
        var diet = await _serviceManager.CoachService.GetDietForTraineeAsync(traineeId);
        return Ok(diet);
    }

    [HttpPut("diet/{dietId}")]
    public async Task<IActionResult> UpdateDietById(int dietId, MealScheduleUpdateDto dto)
    {
        var result = await _serviceManager.CoachService.UpdateDietAsync(dietId, dto);
        return result ? StatusCode(202, "Diet updated successfully.") : StatusCode(500, "An error occurred while processing your request, Please try again."); ;
    }

    [HttpDelete("diet/{dietId}")]
    public async Task<IActionResult> DeleteDietById(int dietId)
    {
        var result = await _serviceManager.CoachService.DeleteDietAsync(dietId);
        return result ? StatusCode(204, "Diet deleted successfully.") : StatusCode(500, "An error occurred while processing your request, Please try again."); ;
    }
    #endregion

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetCoachesBygem(int id)
    {

        var Coaches = await _serviceManager.CoachService.GetCoachesbyGym(id);
        if (Coaches.Any())
            return Ok(Coaches);

        else
            return StatusCode(500, "An error occurred, Please try again.");

    }

    #region Excercise Schdule for Trainee
    [HttpPost("exercise-schedule/{traineeId:int}")]
    public async Task<IActionResult> CreateExerciseScheduleForTrainee(int traineeId, ExerciseScheduleDto exerciseScheduleDto)
    {
        var result = await _serviceManager.CoachService.CreateExerciseScheduleAsync(traineeId, exerciseScheduleDto);

        if (result)
        {
            return Ok(new { Message = "Exercise schedule created successfully." });
        }

        return BadRequest("Failed to create exercise schedule.");
    }

    [HttpGet("exercise-schedule/{scheduleId:int}")]
    public async Task<IActionResult> GetExerciseScheduleById(int scheduleId)
    {
        var schedule = await _serviceManager.CoachService.GetExerciseScheduleByIdAsync(scheduleId);
        return schedule is null ? NotFound() : Ok(schedule);
    }

    [HttpGet("exercise-schedules/{traineeId:int}")]
    public async Task<IActionResult> GetExerciseSchedulesForTrainee(int traineeId)
    {
        var schedule = await _serviceManager.CoachService.GetExerciseSchedulesForTraineeAsync(traineeId);
        return Ok(schedule);
    }

   [HttpPut("exercise-schedule/{scheduleId:int}")]
      public async Task<IActionResult> UpdateExerciseScheduleById(int scheduleId, ExerciseScheduleUpdateDto dto)
      {

          var result = await _serviceManager.CoachService.UpdateExerciseScheduleAsync(scheduleId, dto);
          return result ? Ok("Exercise schedule updated successfully.") : BadRequest("Failed to update schedule.");
      }

   [HttpDelete("exercise-schedule/{scheduleId:int}")]
      public async Task<IActionResult> DeleteExerciseScheduleById(int scheduleId)
      {

          var result = await _serviceManager.CoachService.DeleteExerciseScheduleAsync(scheduleId);
          return result ? Ok("Exercise schedule deleted successfully.") : BadRequest("Failed to delete schedule.");
      }
      #endregion

        [HttpGet("Dashboard/{coachId:int}")]
        public async Task<IActionResult> GetCoachDashboard(int coachId)
        {
            var result = await _serviceManager.CoachService.GetCoachDashboardAsync(coachId);
            return Ok(result);
        }

        [HttpGet("Dashboard/traineeDetails/{traineeId:int}")]
        public async Task<IActionResult> GetTraineeDetails(int traineeId)
        {
            var traineeDetails = await _serviceManager.CoachService.GetTraineeDetailsForDashboardAsync(traineeId);
            return Ok(traineeDetails);
        }

    [HttpGet("{username}")]
    public async Task<ActionResult<CoachInfoResultDto>> GetCoachInfo(string username)
    => Ok(await _serviceManager.CoachService.GetCoachbyUserName(username));

    [HttpGet("muscles")]
    public async Task<IActionResult> GetAllMusclesWithExercises()
    {
        var muscles = await _serviceManager.CoachService.GetAllMusclesWithExercisesAsync();
        return Ok(muscles);
    }
}
