using Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Shared.Auth;

//public record RegisterCoachDto(
//    string FirstName,
//    string LastName,
//    string UserName,
//    string Email,
//    string Password,
//    string PhoneNumber,
//    string Role,
//    AddressDto Address,
//    DateOnly DateOfBirth,
//    IFormFile CV,
//    IFormFile Photo
//    );

//i will add certificaties later 
//DateOnly DateOfBirth
//string PhoneNumber,
public class RegisterCoachDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string Role { get; set; }
    public DateOnly DateOfBirth { get; set; }
    public AddressDto Address { get; set; }
    public IFormFile CV { get; set; }
    public IFormFile Photo { get; set; }

    public CoachSpecialization Specializations { get; set; }
}