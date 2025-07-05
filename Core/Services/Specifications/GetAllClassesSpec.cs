using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GetAllClassesSpec: SpecificationBase<Class,int>
    {
        public GetAllClassesSpec()
        {
            AddIncludes($"{nameof(Coach)}.{nameof(AppUser)}");
            AddIncludes(c => c.Gym);
        }
    }
}
