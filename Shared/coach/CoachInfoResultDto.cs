namespace Shared.coach
{
    public class CoachInfoResultDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public string Specializations { get; set; }
        public AddressDto Address { get; set; }
        public string PhotoUrl { get; set; }

        public int Age { get; set; }


    }
}
