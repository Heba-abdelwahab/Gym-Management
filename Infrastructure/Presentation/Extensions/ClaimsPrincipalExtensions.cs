using System.Security.Claims;

namespace Presentation.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string GetUserName(this ClaimsPrincipal user)
     => user.FindFirstValue(ClaimTypes.Name)!;

    public static int GetUserId(this ClaimsPrincipal user)
     => int.Parse(user.FindFirstValue(ClaimTypes.Sid)!);

    public static string GetAppUserId(this ClaimsPrincipal user)
    => user.FindFirstValue(ClaimTypes.NameIdentifier)!;
}
