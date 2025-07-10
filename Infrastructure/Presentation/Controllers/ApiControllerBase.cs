using Microsoft.AspNetCore.Mvc;
using Presentation.Filters;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
[LogUserActivity]
public class ApiControllerBase : ControllerBase
{
}
