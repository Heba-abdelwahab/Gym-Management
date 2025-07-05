using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{

    public record GymUpdateDto(
    //gym basic info
    [Required]
    string Name,
    [Required]
    [Phone(ErrorMessage ="Invalid phone number format.")]
    string Phone,
    [Required]
    string Description,
    [Required]
    [Range(0,2,ErrorMessage ="rang must be from 0 to 2.")]
    GymType GymType,
    [Required]
    int GymOwnerId,
    //IFormFile? GymImage,
    AddressDto Address
    );
    
}
