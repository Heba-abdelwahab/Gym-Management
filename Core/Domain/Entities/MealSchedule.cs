using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class MealSchedule : EntityBase<int>
    {
        public Schedule schedule { get; set; }
        public int CoachId { get; set; }
        public int TraineeId { get; set; }
        public Coach Coach { get; set; }
        public Trainee Trainee { get; set; }

        public ICollection<Meal>Meals{ get; set; } = new List<Meal>();
    }
}
