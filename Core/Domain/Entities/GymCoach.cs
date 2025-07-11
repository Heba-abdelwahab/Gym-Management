using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class GymCoach : EntityBase<int>
    {
        public double Salary { get; set; } = 0.0;
        public ICollection<WorkDay> WorkDays { get; set; } = new HashSet<WorkDay>();
        public RequestStatus Status { get; set; } = RequestStatus.Pending;
        public int Capcity { set; get; }
        public int CurrentCapcity { set; get; }
        #region Navigation Properties

        public int GymId { get; set; }
        public Gym? Gym { get; set; }
        public int CoachId { get; set; }
        public Coach? Coach { get; set; }

        #endregion
    }
}
