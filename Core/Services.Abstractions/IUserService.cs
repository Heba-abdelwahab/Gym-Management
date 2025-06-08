using Shared;
namespace Services.Abstractions;

public interface IUserService
{


    Task<int?> GetUserIdAsync();
    public string? UserEmail { get; }

    Task<IEnumerable<UserDto>> GetCairoUser();


}
