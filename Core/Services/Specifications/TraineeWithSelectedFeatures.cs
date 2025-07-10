using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class TraineeWithSelectedFeatures : SpecificationBase<Trainee, int>
    {
        public TraineeWithSelectedFeatures(int TraineeId): base(t=>t.Id == TraineeId)
        {
            AddIncludes(t=>t.TraineeSelectedFeatures);
        }
    }
}
