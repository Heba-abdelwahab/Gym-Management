using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    public class GetCoachByIdSpec:SpecificationBase<Coach, int>
    {
        public GetCoachByIdSpec(int id):base(c => c.Id == id)
        {
            AddIclude(c => c.AppUser);
        }
    }
}
