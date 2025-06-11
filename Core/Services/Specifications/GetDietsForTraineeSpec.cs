using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class GetDietsForTraineeSpec : SpecificationBase<MealSchedule, int>
    {
        public GetDietsForTraineeSpec(int traineeId) : base(d => d.TraineeId == traineeId)
        {
            AddIncludes(d => d.Meals);
        }
    }
}
