using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record GymExtraFeatureDto
    (
        string Name ,
        string Image,
        string Description ,
        decimal Cost ,
        int? FeatureId,
        bool IsExtra

    );
}
