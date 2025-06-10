using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class ClassWithTraineesSpec : SpecificationBase<Class, int>
    {
        public ClassWithTraineesSpec(int classId)
            : base(x => x.Id == classId)
        {
            AddIncludes(C => C.Trainees);
        }
    }
}
