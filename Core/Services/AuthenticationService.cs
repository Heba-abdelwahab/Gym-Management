using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Services.Abstractions;
using Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Services;

internal sealed class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;

    public AuthenticationService(UserManager<AppUser> userManager,
         ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }
    public async Task<UserResultDto> RegisterUserAsync(RegisterUserDto registerModel)
    {


        var user = new AppUser
        {
            UserName = registerModel.UserName,
            Email = registerModel.Email,
        };

        var result = await _userManager.CreateAsync(user, registerModel.Password);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description).ToList();

            throw new ValidationException(errors, "Registration failed.");
        }




        //we need to seed roles first zehahhahaha 
        //await _userManager.AddToRoleAsync(user, registerModel.role);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email,registerModel.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.Role, registerModel.role ?? string.Empty)
        };


        await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, registerModel.role ?? string.Empty)); //to test something ..

        return new UserResultDto(
         user.Id,
         user.UserName,
         user.Email,
         _tokenService.GenerateAccessToken(authClaims));


    }


    public Task<UserResultDto> LoginUserAsync(LoginUserDto loginModel)
    {
        throw new NotImplementedException();
    }



    public async Task<bool> CheckEmailExist(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user != null;
    }

}