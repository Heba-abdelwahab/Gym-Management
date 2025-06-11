using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<IActionResult> RequestToBecomeCoachAsync(CoachRequestGymDto request)
        {
           
            var result = await _serviceManager.CoachService.RequestToBecomeCoachAsync(request);

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
        public async Task<ActionResult> HandleCoachJobRequest(int gymId,HandleJobRequestDto jobRequestDto)
        {
            await _serviceManager.CoachService.HandleCoachJobRequest(gymId, jobRequestDto);
            return Ok("Job Request is Handled");
        }

        [HttpPost("create-diet/{traineeId:int}")]
        public async Task<IActionResult> CreateDietForTraineeAsync( int traineeId, MealScheduleDto dietDto)
        {
            var result = await _serviceManager.CoachService.CreateDietAsync(traineeId, dietDto);

            if (result)
            {
                return Ok( "Diet created successfully.");
            }

            return BadRequest("Failed to create diet.");
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
        public async Task<IActionResult> GetById(int scheduleId)
        {
            var schedule = await _serviceManager.CoachService.GetExerciseScheduleByIdAsync(scheduleId);
            return schedule is null ? NotFound() : Ok(schedule);
        }

        [HttpGet("exercise-schedules/{traineeId:int}")]
        public async Task<IActionResult> GetAllForTrainee(int traineeId)
        {
            var schedules = await _serviceManager.CoachService.GetExerciseSchedulesForTraineeAsync(traineeId);
            return Ok(schedules);
        }

        [HttpPut("exercise-schedule/{scheduleId:int}")]
        public async Task<IActionResult> Update(int scheduleId, ExerciseScheduleUpdateDto dto)
        {

            var result = await _serviceManager.CoachService.UpdateExerciseScheduleAsync(scheduleId, dto);
            return result ? Ok("Exercise schedule updated successfully.") : BadRequest("Failed to update schedule.");
        }

        [HttpDelete("exercise-schedule/{scheduleId:int}")]
        public async Task<IActionResult> Delete(int scheduleId)
        {

            var result = await _serviceManager.CoachService.DeleteExerciseScheduleAsync(scheduleId);
            return result ? Ok("Exercise schedule deleted successfully.") : BadRequest("Failed to delete schedule.");
        }
        #endregion

    }
}
