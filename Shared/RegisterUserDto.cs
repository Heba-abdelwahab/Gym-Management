namespace Shared;

public record RegisterUserDto(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    string Role);