using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record GymDto(
    
        //gym basic info
        string Name ,
        string Phone ,
        string Description ,
        GymType GymType ,
        int GymOwnerId,
        //IFormFile? GymImage,
        AddressDto Address ,
        IEnumerable<GymExtraFeatureDto> gymExtraFeatures,
        IEnumerable<GymExtraFeatureDto> gymFeatures
    );
    
}
