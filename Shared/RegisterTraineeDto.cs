namespace Shared;

public record RegisterTraineeDto(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    double? Weight,
    string? ReasonForJoining,
    DateOnly DateOfBirth,
    string Role,
    AddressDto Address
    );
