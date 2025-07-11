using Shared;

namespace Services.Abstractions;

public interface IAdminService
{
    Task<AuthAdminResultDto> CreateAdminAsync(RegisterUserDto request);
    Task<AdminDashboardDto> GetDashboardInfoAsync();
}
