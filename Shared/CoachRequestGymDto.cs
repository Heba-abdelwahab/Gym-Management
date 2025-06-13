using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class CoachRequestGymDto
    {
        public int gymId { get; set; }
        public HashSet<WorkDayDto> workDayDtos { get; set; }            
    }
}
