using Domain.Contracts;
using Domain.Entities;

namespace Services.Specifications
{
    internal class GetGymsForOwner : SpecificationBase<Gym, int>
    {
        public GetGymsForOwner(int ownerId): base(g => g.GymOwnerId == ownerId)
        {}
    }
}
