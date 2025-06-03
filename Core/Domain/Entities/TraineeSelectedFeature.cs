using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TraineeSelectedFeature:EntityBase<int>
    {
        public int SessionCount { get; set; }
        public double TotalCost { get; set; }
        public int TraineeId { get; set; }
        public int GymFeatureId { get; set; }
        public Trainee Trainee { get; set; }
        public GymFeature GymFeature { get; set; }

    }
}
