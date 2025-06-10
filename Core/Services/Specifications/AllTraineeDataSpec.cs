using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class AllTraineeDataSpec : SpecificationBase<Trainee,int>
    {
        public AllTraineeDataSpec(int traineeId) : base(t => t.Id == traineeId)
        {
            AddIncludes(T=>T.Gym);
            AddIncludes("Membership.Features.Feature");
            AddIncludes("TraineeSelectedFeatures.GymFeature.Feature");
            AddIncludes(T=>T.Classes);
        }
    }
}
