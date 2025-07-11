using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Coach : EntityBase<int>
    {
        //public string CV { get; set; }       // file pdf Path
        public CoachSpecialization Specializations { get; set; } = CoachSpecialization.None; // select list 
        public string About { get; set; } = string.Empty;
        public Address Address { get; set; } = null!;


        #region New Prop
        public string? AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = null!;
        public DateOnly? DateOfBirth { get; set; }


        #region upload image 
        public MediaValueObj Image { get; set; } // upload when register   
        #endregion

        #region CV
        /// <summary>
        /// save into server , path in url of media 
        /// </summary>

        public MediaValueObj CV { get; set; }
        #endregion


        #endregion

        //public ICollection<Media>? Certificates { get; set; } = new HashSet<Media>();






        #region Navigation Property
        public ICollection<GymCoach> GymCoaches { get; set; } = new HashSet<GymCoach>();
        public ICollection<ExercisesSchedule> ExercisesSchedules { get; set; } = new HashSet<ExercisesSchedule>();
        public ICollection<MealSchedule> MealSchedules { get; set; } = new HashSet<MealSchedule>();

        public int CurrentCapcity { set; get; }
        public ICollection<Trainee> Trainees { get; set; } = new HashSet<Trainee>();
        public ICollection<Class> Classes { get; set; } = new HashSet<Class>();
        #endregion
    }


}
