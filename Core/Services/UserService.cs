using Domain.Constants;
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

    public UserService(IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork)
    {
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
    }


    public async Task< int?> GetUserIdAsync() {

        string AppUserId=  _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        string role = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Role);

        if (role == Roles.Coach)
        {
            Coach coach = await _unitOfWork.GetRepositories<Coach, int>().GetByIdWithSpecAsync(new CoachByAppUserIdSpec(AppUserId));
            return coach.Id;
        }
        else if (role == Roles.Trainee) {
            Trainee trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdWithSpecAsync(new TraineeByAppUserIdSpec(AppUserId));
            return trainee.Id;
        }
        else if (role == Roles.Owner)
        {
            GymOwner gymOwner = await _unitOfWork.GetRepositories<GymOwner, int>().GetByIdWithSpecAsync(new GymOwnerByAppUserIdSpec(AppUserId));
            return gymOwner.Id;
        }
        else if (role == Roles.Admin)
        {
            Admin admin = await _unitOfWork.GetRepositories<Admin, int>().GetByIdWithSpecAsync(new AdminByAppUserIdSpec(AppUserId));
            return admin.Id;
        }
        return null;
    }

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
