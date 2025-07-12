using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications.GymOwnerSpec;

internal class GetGymOwnerByAppUserIdSpec : SpecificationBase<GymOwner, int>
{

    public GetGymOwnerByAppUserIdSpec(string id)
        : base(g => g.AppUserId == id)
    {

        AddIncludes(g => g.AppUser);
    }


}
