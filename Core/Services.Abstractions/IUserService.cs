using Shared;
namespace Services.Abstractions;

public interface IUserService
{


    public string? Id { get; }
    public string? UserEmail { get; }

    Task<IEnumerable<UserDto>> GetCairoUser();


}
