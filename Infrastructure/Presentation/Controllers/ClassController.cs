using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentation.Controllers
{
    public class ClassController : ApiControllerBase
    {
        private IServiceManager _serviceManager;

        public ClassController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            var classes = await _serviceManager.ClassService.GetAllClassesAsync();
            return Ok(classes);
        }

        [HttpGet("Gym/{gymId:int}")]
        public async Task<IActionResult> GetClassesByGym(int gymId)
        {
            var classes = await _serviceManager.ClassService.GetClassesByGymAsync(gymId);
            return Ok(classes);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetClassById(int id)
        {
            var selectedClass = await _serviceManager.ClassService.GetClassByIdAsync(id);
            return Ok(selectedClass);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClass(ClassDto newClass)
        {
            var addedClass = await _serviceManager.ClassService.CreateClassAsync(newClass);
            return CreatedAtAction(nameof(GetClassById), new {id = addedClass.Id}, addedClass);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateClass(int id, ClassDto updatedClassDto)
        {
            var updatedClass = await _serviceManager.ClassService.UpdateClassAsync(id, updatedClassDto);
            return Ok(updatedClass);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteClass(int id)
        {
            await _serviceManager.ClassService.DeleteClassByIdAsync(id);
            return Ok($"Class with Id: {id} Deleted Successfully.");
        }

        [HttpGet("{classId:int}/Trainees")]
        public async Task<IActionResult> GetClassTrainees(int classId)
        {
            var trainees = await _serviceManager.ClassService.GetClassTraineesAsync(classId);
            return Ok(trainees);
        }

        [HttpDelete("{classId:int}/Trainee/{traineeId:int}")]
        public async Task<IActionResult> RemoveTraineeFormClass(int classId, int traineeId)
        {
            await _serviceManager.ClassService.RemoveTraineeFromClassAsync(classId, traineeId);
            return Ok($"Trainee with Id: {traineeId} Deleted Successfully from Class with Id: {classId}.");
        }

        [HttpGet("{classId:int}/Trainee/{traineeId:int}")]
        public async Task<IActionResult> AddTraineeToClass(int classId, int traineeId)
        {
            var trainee = await _serviceManager.ClassService.AddTraineeToClassAsync(classId, traineeId);
            return Ok(trainee);
        }

        [HttpGet("{classId:int}/notJoinedTrainees")]
        public async Task<IActionResult> GetTraineesNotInClassAsync(int classId)
        {
            var notJoinedTrainees = await _serviceManager.ClassService.GetTraineesNotInClassAsync(classId);
            return Ok(notJoinedTrainees);
        }
    }
}
