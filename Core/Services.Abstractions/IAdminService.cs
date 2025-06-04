using Shared;

namespace Services.Abstractions;

public interface IAdminService
{

    Task<AdminResultDto> CreateAdminAsync(RegisterUserDto request);


}
