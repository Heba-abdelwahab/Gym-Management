using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record HandleJobRequestDto(int CoachId , bool IsAccepted);
}
