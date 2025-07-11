using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications.CoachSpec
{
    public class GetCoachByAppUserIdSpec : SpecificationBase<Coach, int>
    {
        public GetCoachByAppUserIdSpec(string id)
            : base(c => c.AppUserId == id)
        {
            AddIncludes(c => c.AppUser);
        }
    }
}
