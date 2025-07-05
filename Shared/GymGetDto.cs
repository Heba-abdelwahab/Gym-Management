using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record GymGetDto
    (
        AddressDto Address ,
        GymType GymType ,
        string Media ,
        string Name ,
        string Phone ,
        string Description 
    );
}
