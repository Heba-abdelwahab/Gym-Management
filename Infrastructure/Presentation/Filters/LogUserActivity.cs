using Domain.Contracts;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Extensions;

namespace Presentation.Filters;

public class LogUserActivity : ActionFilterAttribute
{



    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var resultContext = await next?.Invoke();

        //if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

        var userId = resultContext.HttpContext.User.GetAppUserId();

        var userRepo = resultContext.HttpContext.RequestServices.GetRequiredService<IUserRepository>();

        var user = await userRepo.GetUserByIdAsync(userId);

        if (user is null) return;

        user.LastActive = DateTime.UtcNow;

        await userRepo.SaveAllAsync();


    }
}
