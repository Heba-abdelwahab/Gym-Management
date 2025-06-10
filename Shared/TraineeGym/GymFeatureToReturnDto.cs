using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TraineeGym
{
    public class GymFeatureToReturnDto
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public string Name { get; set; }
        public bool IsExtra { get; set; }

    }
}
