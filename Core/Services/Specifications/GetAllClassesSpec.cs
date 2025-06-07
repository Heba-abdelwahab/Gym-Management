using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GetAllClassesSpec: SpecificationBase<Class,int>
    {
        public GetAllClassesSpec()
        {
            AddIclude(c => c.Coach);
            AddIclude(c => c.Gym);
        }
    }
}
