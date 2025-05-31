using Domain.Common;
using Domain.ValueObjects;
namespace Domain.Entities
{
    public class Trainee:EntityBase<Trainee>
    {
        public required Address Address { get; set; }
        public required string Image { get; set; }

        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }

    }
}
