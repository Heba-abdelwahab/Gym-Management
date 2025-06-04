using Shared;
namespace Services.Abstractions;

public interface IUserService
{


    string? Id { get; }
    string? UserEmail { get; }

    Task<IEnumerable<UserDto>> GetCairoUser();


}
