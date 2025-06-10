using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TraineeGym
{
    public class GymToReturnDto
    {
        public GymType GymType { get; set; }
        public string Media { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public AddressToReturnDto Address { get; set; }
    }
}
