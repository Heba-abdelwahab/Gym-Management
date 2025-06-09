using System.Security.Claims;

namespace Services.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    List<Claim> GenerateAuthClaims<TKey>(TKey Id, string UserName, string Email, string Role);
}
