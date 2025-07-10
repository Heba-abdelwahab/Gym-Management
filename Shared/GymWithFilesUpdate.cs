using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record GymWithFilesUpdate(
        string gymInfo,
        IFormFile? Media,
        IEnumerable<IFormFile>? GymImages
    );
}
