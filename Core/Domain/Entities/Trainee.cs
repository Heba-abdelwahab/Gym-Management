using Domain.Common;
using Domain.ValueObjects;
namespace Domain.Entities
{
    public class Trainee:EntityBase<int>
    {
        public required Address Address { get; set; }
        public required string Image { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }

        public string AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public int GymId { get; set; }
        public int MembershipId { get; set; }
        public int? CoachId { get; set; }

        public Membership Membership { get; set; }
        public Gym Gym { get; set; }
        public Coach? Coach { get; set; }

        public ICollection<TraineeSelectedFeature> TraineeSelectedFeatures { get; set; } = new List<TraineeSelectedFeature>();
        public ICollection<Class> Classes { get; set; } = new List<Class>();
        public ICollection<ExercisesSchedule> ExercisesSchedules { get; set; } = new List<ExercisesSchedule>();
        public ICollection<MealSchedule> MealSchedules { get; set; } = new List<MealSchedule>();

    }
}
