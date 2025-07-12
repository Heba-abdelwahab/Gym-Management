using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TraineeGym
{
    public class ClassGymWithCoachToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Capacity { get; set; }
        public int CurrentCapacity { get; set; }
        public CoachToReturnDto? Coach { get; set; }
        public GymToReturnDto? Gym { get; set; }
    }
}
