using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GymCoach:EntityBase<int>
    {
        public double Salary { get; set; }
        public ICollection<WorkDay> WorkDays { get; set; } = new List<WorkDay>();
        public int GymId { get; set; }
        public int CoachId { get; set; }
        public Gym Gym { get; set; }
        public Coach Coach { get; set; }
    }
}
