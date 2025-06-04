using System.Security.Claims;

namespace Services.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
}
