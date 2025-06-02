using Domain.Common;
using Domain.ValueObjects;
namespace Domain.Entities.Identity
{
    public class Trainee : AppUser
    {
        //public AppUser AppUser { get; set; }
        public Address? Address { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipEndDate { get; set; }
    }
}
