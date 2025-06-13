using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record GymExtraFeatureDto
    (
        int? FeatureId,
        string Image,
        string Description ,
        decimal Cost ,
        string Name ,
        bool IsExtra

    );
}
