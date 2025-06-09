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
    }
}
