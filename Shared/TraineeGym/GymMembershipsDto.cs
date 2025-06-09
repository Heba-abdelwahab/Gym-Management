using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TraineeGym
{
    public class GymMembershipsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Duration { get; set; } 
        public int Count { get; set; }
        public ICollection<FeatureDto> Features { get; set; } = new List<FeatureDto>();
    }
}
