using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record GymWithFilesDto
    (
        string gymInfo,
        IEnumerable<IFormFile>?GymExtraFeaturesImages,
        IEnumerable<IFormFile>? GymFeaturesImages,
        IFormFile Media,
        IEnumerable<IFormFile> GymImages
    );
}
