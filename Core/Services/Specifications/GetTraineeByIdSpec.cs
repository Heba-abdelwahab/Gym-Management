using Domain.Contracts;
using Domain.Entities;


namespace Services.Specifications
{
    public class GetTraineeByIdSpec : SpecificationBase<Trainee, int>
    {
        public GetTraineeByIdSpec(int id)
            : base(t => t.Id == id)
        {
            AddIncludes(t => t.AppUser);
        }
    }
}
