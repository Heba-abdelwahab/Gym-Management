namespace Shared;

public record AuthUserLoginResultDto(
    string UserName,
    string Token,
    string PhotoUrl,
    string KnownAs
    );
