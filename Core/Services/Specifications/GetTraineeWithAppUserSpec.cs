using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    internal class GetTraineeWithAppUserSpec : SpecificationBase<Trainee, int>
    {
        public GetTraineeWithAppUserSpec(int TraineeId): base(t => t.Id == TraineeId)
        {
            AddIncludes("AppUser.Photos");
        }
    }
}
