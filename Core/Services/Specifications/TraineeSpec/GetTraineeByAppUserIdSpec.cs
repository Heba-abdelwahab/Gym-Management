using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications.TraineeSpec;

internal class GetTraineeByAppUserIdSpec : SpecificationBase<Trainee, int>
{

    public GetTraineeByAppUserIdSpec(string id)
           : base(t => t.AppUserId == id)
    {
        AddIncludes(t => t.AppUser);
        AddIncludes("Coach.AppUser");
        AddIncludes(t => t.Gym);

    }
}
