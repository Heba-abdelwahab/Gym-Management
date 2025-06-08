using Domain.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using System.Security.Claims;
namespace Services;

internal sealed class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUnitOfWork _unitOfWork;

    public UserService(IHttpContextAccessor httpContextAccessor,
        IUnitOfWork unitOfWork)
    {
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }


    public int? Id => int.Parse(_httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)!);

    public string? UserEmail => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Email);


    public async Task<IEnumerable<UserDto>> GetCairoUser()
    {
        IRepository<User, int> repo = _unitOfWork.GetRepositories<User, int>();
        var users = await repo.GetAllWithSpecAsync(new GetCairoUsersSpec());
        List<UserDto> userDtos = new List<UserDto>();
        foreach (var user in users)
        {
            UserDto userDto = new UserDto()
            {
                Address = user.address,
                Id = user.Id,
                Name = user.name
            };
            userDtos.Add(userDto);
        }
        return userDtos;
    }





}
