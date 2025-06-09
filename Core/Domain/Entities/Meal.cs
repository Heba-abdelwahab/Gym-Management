using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Meal:EntityBase<int>
    {
        public string Description { get; set; }
        public DateTime Day { get; set; }
        public int MealScheduleId { get; set; }
        public MealType MealType { get; set; }
        public MealSchedule? MealSchedule { get; set; }

    }
}
