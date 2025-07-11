namespace Shared.Auth;

public record RegisterTraineeDto(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    double? Weight,
    string? ReasonForJoining,
    DateOnly DateOfBirth,
    string PhoneNumber,
    string Role,
    AddressDto Address
    );
