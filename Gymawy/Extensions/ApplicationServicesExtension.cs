using Domain.Contracts;
using Persistence.Repositories;
using Services;
using Services.Abstractions;

namespace Gymawy.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection Services)
        {

            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            Services.AddScoped(typeof(IAuthService), typeof(AuthService));

            return Services;
        }
    }
}
