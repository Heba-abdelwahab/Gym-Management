using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Feature:EntityBase<int>
    {
        public string Name { get; set; }
        ICollection<GymFeature> GymFeatures { get; set; } = new List<GymFeature>();  //common+extra
        ICollection<TraineeSelectedFeature> TraineeSelectedFeatures { get; set; } = new List<TraineeSelectedFeature>();
    }
}
