using Gymawy.Middlewares;

namespace Gymawy.Extensions;

public static class WebApplicationExtensions
{

    public static WebApplication UseCustomExceptionMiddleware(this WebApplication app)
    {
        app.UseMiddleware<GlobalErrorHandlingMiddleware>();
        return app;
    }


}
