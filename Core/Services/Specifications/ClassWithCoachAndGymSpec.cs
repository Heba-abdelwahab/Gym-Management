using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ClassWithCoachAndGymSpec : SpecificationBase<Class, int>
    {
        public ClassWithCoachAndGymSpec()
            : base()
        {
            AddIncludes("Coach.AppUser");
            AddIncludes(c => c.Gym);
        }
    }
}
