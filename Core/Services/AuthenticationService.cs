using Domain.Constants;
using Domain.Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Services.Specifications.CoachSpec;
using Services.Specifications.GymOwnerSpec;
using Services.Specifications.TraineeSpec;
using Shared;
using Shared.Auth;
using System.Security.Claims;

namespace Services;

internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public AuthenticationService(UserManager<AppUser> userManager,
         ITokenService tokenService, IUnitOfWork unitOfWork)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
    }
    public async Task<AuthUserResultDto> RegisterUserAsync(RegisterUserDto registerModel)
    {
        var existingEmail = await CheckEmailExist(registerModel.Email);

        if (existingEmail) // i will change it later ** , 
            throw new ValidationException
                ([$"this email {registerModel.Email} is already taken"], "Registration failed.");

        var user = new AppUser
        {
            FirstName = registerModel.FirstName,
            LastName = registerModel.LastName,
            UserName = registerModel.UserName,
            Email = registerModel.Email,
            PhoneNumber = registerModel.PhoneNumber
        };

        var result = await _userManager.CreateAsync(user, registerModel.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();

            throw new ValidationException(errors, "Registration failed.");
        }




        //we need to seed roles first zehahhahaha 
        //await _userManager.AddToRoleAsync(user, registerModel.role);

        #region Old Using Normal Identity ID
        //var authClaims = new List<Claim>
        //{
        //    new(ClaimTypes.NameIdentifier, user.Id),
        //    new(ClaimTypes.Name, user.UserName),
        //    new(ClaimTypes.Email,registerModel.Email),
        //    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        //    new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
        //        ClaimValueTypes.Integer64),
        //    new Claim(ClaimTypes.Role, registerModel.role ?? string.Empty)
        //}; 
        #endregion


        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, registerModel.Role ?? string.Empty)); //to test something ..


        return new AuthUserResultDto(
         user.Id,
         user.UserName,
         user.Email);


    }


    public async Task<AuthUserLoginResultDto> LoginUserAsync(LoginUserDto loginModel)
    {
        //var user = await _userManager.FindByEmailAsync(loginModel.Email);

        var user = await _userManager.Users
                         .Include(u => u.Photos)
                         .SingleOrDefaultAsync(u => u.Email == loginModel.Email);


        if (user is null) throw new UnAuthorizedException();

        var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);

        if (!result) throw new UnAuthorizedException();

        var claims = await _userManager.GetClaimsAsync(user);
        var roleValue = claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;


        GymOwner owner = null;
        Trainee trainee = null;
        Coach coach = null;
        Admin admin = null;
        int id = 0;
        #region need it later 
        if (roleValue is Roles.Owner)
        {
            owner = await _unitOfWork.GetRepositories<GymOwner, int>().GetByIdWithSpecAsync(new GetGymOwnerByAppUserIdSpec(user.Id));
            id = owner.Id;

        }
        if (roleValue is Roles.Trainee)
        {
            trainee = await _unitOfWork.GetRepositories<Trainee, int>().GetByIdWithSpecAsync(new GetTraineeByAppUserIdSpec(user.Id));
            id = trainee.Id;

        }
        if (roleValue is Roles.Coach)
        {
            coach = await _unitOfWork.GetRepositories<Coach, int>().GetByIdWithSpecAsync(new GetCoachByAppUserIdSpec(user.Id));
            id = coach.Id;
        }
        //if (roleValue is Roles.Owner)
        //{
        //    admin = await _unitOfWork.GetRepositories<GymOwner, int>().GetByIdWithSpecAsync(new GetGymOwnerByAppUserIdSpec(user.Id));

        //}
        #endregion


        var authClaims = _tokenService.GenerateAuthClaims(
                    id, user.Id, user.UserName,
                     user.Email, roleValue);



        return new AuthUserLoginResultDto(
          UserName: user.UserName,
          Token: _tokenService.GenerateAccessToken(authClaims),
          PhotoUrl: user.Photos.FirstOrDefault(p => p.IsMain)?.Url!,
          KnownAs: $"{user.FirstName} {user.LastName}",
          Role: roleValue
          );
    }



    public async Task<bool> CheckEmailExist(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user != null;
    }



}