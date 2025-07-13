using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TraineeGym
{
    public class EditTraineeProfileDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressToReturnDto? Address { get; set; }
        public IFormFile? Image { get; set; }
        public DateOnly? DateOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string ReasonForJoining { get; set; }
        public double? Weight { get; set; }
    }
}
