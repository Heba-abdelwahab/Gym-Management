using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Shared
{
    public record GymDto(
        //gym basic info
        [Required]
        string Name ,
        [Required]
        [Phone(ErrorMessage ="Invalid phone number format.")]
        string Phone ,
        [Required]
        string Description ,
        [Required]
        [Range(0,2,ErrorMessage ="rang must be from 0 to 2.")]
        GymType GymType ,
        [Required]
        int GymOwnerId,
        //IFormFile? GymImage,
        AddressDto Address ,
        IEnumerable<ExGymFeatureDto>?GymExtraFeatures,
        IEnumerable<NonExGymFeatureDto>?GymFeatures
    );
    
}
