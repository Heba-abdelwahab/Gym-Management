using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.coach
{
    public record CoachRequestToGymDto
    {
        public int GymId { get; init; }
        public List<CoachWorkDayDto> WorkDays { get; init; }
    }
    public record CoachWorkDayDto
    {
        public TimeOnly Start { get; init; }
        public TimeOnly End { get; init; }
        public DayOfWeek Day { get; init; }
    }
}
