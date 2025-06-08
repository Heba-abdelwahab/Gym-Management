namespace Shared;

public record RegisterCoachDto(
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string Password,
    string Role,
    AddressDto Address,
    DateOnly DateOfBirth
    );

//i will add certificaties later 
//DateOnly DateOfBirth
//string PhoneNumber,
