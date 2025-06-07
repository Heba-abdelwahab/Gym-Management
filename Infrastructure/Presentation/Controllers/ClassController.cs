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
        public async Task<IActionResult> CreateClassAsync(ClassDto newClass)
        {
            var addedClass = await _serviceManager.ClassService.CreateClassAsync(newClass);
            return CreatedAtAction(nameof(GetClassById), new {id = addedClass.Id}, addedClass);
        }
    }
}
