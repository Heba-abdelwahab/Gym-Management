using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TraineeGym
{
    public class TraineeDataToReturnDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public AddressToReturnDto Address { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public DateOnly? DateOfBirth { get; set; }
        public double? Weight { get; set; }
    }
}
