using Domain.Common;
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
        public string Name { get; set; }
        public string CV { get; set; }       // file pdf Path
        public string About { get; set; }              
        public string ProfileImage { get; set; }       // Image path
        public Address Address { get; set; }


        public ICollection<GymCoach> GymCoaches { get; set; } = new List<GymCoach>();
        public ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();
        public ICollection<Class> Classes { get; set; } = new List<Class>();
        public ICollection<ExercisesSchedule> ExercisesSchedules { get; set; } = new List<ExercisesSchedule>();
        public ICollection<MealSchedule> MealSchedules { get; set; } = new List<MealSchedule>();

    }
}
