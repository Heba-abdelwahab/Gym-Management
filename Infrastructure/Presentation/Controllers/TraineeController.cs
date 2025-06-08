using Services.Abstractions;

namespace Presentation.Controllers;

public class TraineeController : ApiControllerBase
{


    private readonly IServiceManager _serviceManager;

    public TraineeController(IServiceManager serviceManager)
    {

        _serviceManager = serviceManager;
    }




}
