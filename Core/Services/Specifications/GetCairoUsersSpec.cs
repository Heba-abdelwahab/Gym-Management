using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GetCairoUsersSpec : SpecificationBase<User, int>
    {
        public GetCairoUsersSpec() : base(u => u.address == "cairo")
        {
            ApplyOrderBy(u => u.name);
            ApplyTake(2);
        }

    }
}
