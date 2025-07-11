using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers;

public class AdminController : ApiControllerBase
{
    private IServiceManager _serviceManager;
    public AdminController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpGet("Dashboard")]
    public async Task<IActionResult> GetDashboard()
    {
        var info = await _serviceManager.AdminService.GetDashboardInfoAsync();

        return Ok(info);
    }
}
