using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class GetExerciseSchedulesForTraineeSpec : SpecificationBase<ExercisesSchedule, int>
    {
        public GetExerciseSchedulesForTraineeSpec(int traineeId) : base(s => s.TraineeId == traineeId)
        {
            AddIncludes(s => s.MuscleExerices);
        }
    }
}
