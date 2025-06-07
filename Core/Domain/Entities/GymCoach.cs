using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GymCoach:EntityBase<int>
    {
        public double Salary { get; set; } = 0.0;
        public ICollection<WorkDay> WorkDays { get; set; } = new HashSet<WorkDay>();
        public RequestStatus Status { get; set; } = RequestStatus.Pending;

        #region Navigation Properties

        public int GymId { get; set; }
        public Gym? Gym { get; set; }
        public required string CoachId { get; set; }
        public Coach? Coach { get; set; }

        #endregion
    }
}
