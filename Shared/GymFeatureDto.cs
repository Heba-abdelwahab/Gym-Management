using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class GymFeatureDto
    {
        public int? Id { get; init; }
        public string Image { get; init; }
        public string Description { get; init; }
        public decimal Cost { get; init; }
        public string Name { get; init; }
        public bool IsExtra { get; init; }
        public int FeatureId { get; set; }
    }
}
