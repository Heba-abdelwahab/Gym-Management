using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GetClassByIdSpec: SpecificationBase<Class, int>
    {
        public GetClassByIdSpec(int id) : base(c => c.Id == id)
        {
           // AddIncludes(c => c.Coach).theninclude(c => c.Class);
           AddIncludes("Coach.AppUser");
//       AddIncludes($"{nameof(Coach)}.{nameof(Coach.AppUser)}");
            AddIncludes(c => c.Gym);
           
        }
    }
}
