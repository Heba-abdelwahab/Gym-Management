using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications;

internal sealed class GetTraineeByAppUserIdWithCoachAndGymSpec
    : SpecificationBase<Trainee, int>
{

    public GetTraineeByAppUserIdWithCoachAndGymSpec(string AppUserId)
        : base(trainee => trainee.AppUserId == AppUserId)
    {
        AddIncludes(trainee => trainee.Coach!);
        AddIncludes(trainee => trainee.Gym!);
    }


}


//internal sealed class GetTraineeByAppUserIdWithCoachAndGymSpec 
//    : SpecificationBase<Trainee, int>
//{

//    public GetTraineeByAppUserIdWithCoachAndGymSpec(string AppUserId)
//        : base(trainee => trainee.AppUserId == AppUserId)
//    {
//        AddIncludes(trainee => trainee.Coach!);
//        AddIncludes(trainee => trainee.Gym!);
//    }


//}
