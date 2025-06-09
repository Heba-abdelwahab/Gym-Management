using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GetAllClassesSpec: SpecificationBase<Class,int>
    {
        public GetAllClassesSpec()
        {
            AddIncludes(c => c.Coach);
            AddIncludes(c => c.Gym);
        }
    }
}
