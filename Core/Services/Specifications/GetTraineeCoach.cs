using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class GetTraineeCoach : SpecificationBase<Trainee, int>
    {
        public GetTraineeCoach(int traineeId) : base(t => t.Id == traineeId)
        {
            AddIncludes("Coach.AppUser");
            AddIncludes("Coach.Trainees");
        }
    }
}
