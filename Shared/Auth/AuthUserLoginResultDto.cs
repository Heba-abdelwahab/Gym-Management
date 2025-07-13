namespace Shared.Auth;

public record AuthUserLoginResultDto(
    string UserName,
    string Token,
    string PhotoUrl,
    string KnownAs,
    string Role,
    int Id
    );
