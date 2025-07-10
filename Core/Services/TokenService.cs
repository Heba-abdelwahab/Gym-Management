using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstractions;
using Shared.Jwt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services;

internal sealed class TokenService : ITokenService
{
    private readonly IOptionsMonitor<JwtOptions> _jwtOptions;

    public TokenService(IOptionsMonitor<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {


        //var claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.NameIdentifier, user.Id),
        //    new Claim (ClaimTypes.Role , "student") 
        //};


        //await _userManager.AddClaimsAsync(user, claims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.CurrentValue.SecretKey));

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var Expire = DateTime.UtcNow.AddDays(_jwtOptions.CurrentValue.DurationInDays);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: Expire
            );


        //return new TokenDto(new JwtSecurityTokenHandler().WriteToken(token), Expire);

        var userToken = new JwtSecurityTokenHandler().WriteToken(token);

        return userToken;


    }

    public List<Claim> GenerateAuthClaims<TKey>(TKey Id, string AppUserId, string UserName, string Email, string Role)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, AppUserId),
            new(ClaimTypes.Sid, Id?.ToString()!),
            new(ClaimTypes.Name, UserName),
            new(ClaimTypes.Email,Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),
                ClaimValueTypes.Integer64),
            new Claim(ClaimTypes.Role,Role)
        };

        return authClaims;
    }
}
