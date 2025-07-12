using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GetClassesByGymSpec:SpecificationBase<Class, int>
    {
        public GetClassesByGymSpec(int gymId): base(c => c.GymId == gymId)
        {
            AddIncludes(c => c.Gym);
            AddIncludes($"{nameof(Coach)}.{nameof(AppUser)}");
        }
    }
}
