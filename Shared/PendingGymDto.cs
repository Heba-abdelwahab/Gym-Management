using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class PendingGymDto
    {
        public int GymId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public string GymType { get; set; }

    }
}
