using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class GetScheduleByIdSpec : SpecificationBase<ExercisesSchedule, int>
    {
        public GetScheduleByIdSpec(int scheduleId): base(s => s.Id == scheduleId)
        {
            AddIncludes(s => s.ScheduledExercises);
            AddIncludes("ScheduledExercises.Exercise");
            AddIncludes("ScheduledExercises.Exercise.TargetMuscle");
            
        }
    }
}
