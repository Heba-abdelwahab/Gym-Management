using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class GetAllMusclesSpec : SpecificationBase<Muscle, int>
    {
        public GetAllMusclesSpec() : base()
        {
            AddIncludes(m => m.Exercises);
        }
    }
}
