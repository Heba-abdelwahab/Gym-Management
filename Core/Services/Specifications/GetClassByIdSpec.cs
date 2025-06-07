using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GetClassByIdSpec: SpecificationBase<Class, int>
    {
        public GetClassByIdSpec(int id) : base(c => c.Id == id)
        {
            AddIclude(c => c.Coach);
            AddIclude(c => c.Gym);
        }
    }
}
