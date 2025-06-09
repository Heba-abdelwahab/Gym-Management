using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class WorkDayDto
    {
        public TimeOnly Start { get; set; }
        public TimeOnly End { get; set; }
        public DayOfWeek Day { get; set; }
    }
}
