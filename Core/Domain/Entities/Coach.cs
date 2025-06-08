using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Coach:EntityBase<int>
    {
        //public string CV { get; set; }       // file pdf Path
        public CoachSpecialization Specializations { get; set; }
        public string About { get; set; }

        public Address Address { get; set; }

        public string AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public ICollection<Media>? Certificates { get; set; } = new HashSet<Media>();


        #region Navigation Property
        public ICollection<GymCoach> GymCoaches { get; set; } = new HashSet<GymCoach>();
        public ICollection<ExercisesSchedule> ExercisesSchedules { get; set; } = new HashSet<ExercisesSchedule>();
        public ICollection<MealSchedule> MealSchedules { get; set; } = new HashSet<MealSchedule>();


        public ICollection<Trainee> Trainees { get; set; } = new HashSet<Trainee>();
        public ICollection<Class> Classes { get; set; } = new HashSet<Class>();
        #endregion
    }


}
