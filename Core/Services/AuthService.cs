using Domain.Common;
using Domain.Entities.Identity;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Services.Abstractions;
using Shared.AuthDto;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        public async Task<UserToReturnDto?> RegisterAsync(RegisterDto registerDto, string role)
        {
            var user = new Trainee
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                UserName = registerDto.Email.Split('@')[0], // Use email prefix as username
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,

                ///AppUser = new AppUser
                ///{
                ///    FirstName = registerDto.FirstName,
                ///    LastName = registerDto.LastName,
                ///    Email = registerDto.Email,
                ///    PhoneNumber = registerDto.PhoneNumber,
                ///    // ToDo: Add Image Path After Uploading It
                ///},


                Address = new Address
                {
                    Street = registerDto.Street ?? string.Empty,
                    City = registerDto.City ?? string.Empty,
                    Country = registerDto.Country ?? string.Empty
                },
                MembershipStartDate = DateTime.Now,
                MembershipEndDate = DateTime.Now.AddMonths(1) // Default membership duration of 1 Month
            };

            var Result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!Result.Succeeded)
                return null;

            if (role == "Trainee")
                Result = await _userManager.AddToRoleAsync(user, "Trainee");
            
            var ReturnDto = new UserToReturnDto
            {
                DisplayName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                Role = role,
                Token = await CreateTokenAsync(user)
            };
            return ReturnDto;
        }



        public async Task<UserToReturnDto?> LoginAsync(LoginDto loginDto)
        {

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return null;
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
                return null;

            // Get User Roles
            var userRoles = await _userManager.GetRolesAsync(user) as List<string>;
            var token = await CreateTokenAsync(user);
            return new UserToReturnDto
            {
                DisplayName = $"{user.FirstName} {user.LastName}",
                Email = user.Email!,
                Role = userRoles![0],
                Token = token
            };
        }

        public async Task<ActionResult<bool>> CheckIfUserExist(string Email)
        {
            return await _userManager.FindByEmailAsync(Email) is not null;
        }

        private async Task<string> CreateTokenAsync(AppUser user)
        {
            // Claims
            var UserClaim = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("Image", user.Image ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };


            var userRoles = await _userManager.GetRolesAsync(user);

            // Add UserRoles To Claims
            foreach (var role in userRoles)
            {
                UserClaim.Add(new Claim(ClaimTypes.Role, role));
            }

            // Security Key
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

            // Create Token Object
            var Token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["JWT:Expire"])),
                claims: UserClaim,
                signingCredentials: new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature)
                );

            return new JwtSecurityTokenHandler().WriteToken(Token);
        }

    }
}
