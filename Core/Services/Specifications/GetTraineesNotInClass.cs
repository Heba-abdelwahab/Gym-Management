using Domain.Contracts;
using Domain.Entities;


namespace Services.Specifications
{
    public class GetTraineesNotInClass:SpecificationBase<Trainee,int>
    {
        public GetTraineesNotInClass(int classId) : base(t => t.Classes.All(c => c.Id != classId))
        {
            AddIncludes(t => t.AppUser);
        }
    }
}
