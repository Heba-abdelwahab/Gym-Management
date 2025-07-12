using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TraineeGym
{
    public class TraineeCoachToReturnDto
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; } //+Added for solving undefined property for chat
        public string ImageUrl { get; set; }
        public string Specializations { get; set; }
        public string About { get; set; }
        public int TraineeCount { get; set; }

    }
}
