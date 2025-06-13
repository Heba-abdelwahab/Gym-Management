using Domain.Contracts;
using Gymawy.Middlewares;

namespace Gymawy.Extensions;

public static class WebApplicationExtensions
{

    public static WebApplication UseCustomExceptionMiddleware(this WebApplication app)
    {
        app.UseMiddleware<GlobalErrorHandlingMiddleware>();
        return app;
    }
    public static async Task<WebApplication> SeedDbAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

            await dbInitializer.InitializeAsync();
            return app;
        }
    }

}
