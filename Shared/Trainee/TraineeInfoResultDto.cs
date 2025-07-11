namespace Shared.Trainee
{
    public class TraineeInfoResultDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string ReasonForJoining { get; set; }
        public int Age { get; set; }
        public string Gym { get; set; }
        public string Coach { get; set; }
        public string Weight { get; set; }

        public DateTime? MembershipStartDate { get; set; }

        public AddressDto Address { get; set; }
        public string PhotoUrl { get; set; }


    }
}
