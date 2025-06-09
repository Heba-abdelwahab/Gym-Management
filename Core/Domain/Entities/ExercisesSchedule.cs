using Domain.Common;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExercisesSchedule:EntityBase<int>
    {
        public Schedule schedule {  get; set; }
        public int CoachId { get; set; }
        public int TraineeId { get; set; }
        public Coach Coach { get; set; }
        public Trainee Trainee { get; set; }
        public ICollection<MuscleExerices> MuscleExerices { get; set; } = new List<MuscleExerices>();        

    }
}
