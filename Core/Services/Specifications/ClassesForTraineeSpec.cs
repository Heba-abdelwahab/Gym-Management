using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ClassesForTraineeSpec : SpecificationBase<Class,int>
    {
        public ClassesForTraineeSpec(int gymId)
            : base(x => x.GymId == gymId )
        {
            AddIncludes("Coach.AppUser");
            AddIncludes(C => C.Trainees);
        }
    }
}
