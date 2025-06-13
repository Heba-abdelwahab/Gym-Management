using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.TraineeGym
{
    public class TraineeFeatureToReturnDto
    {
        public int Count { get; set; }
        public decimal SessionCost { get; set; }
        public double TotalCost { get; set; }
        public string Name { get; set; }
    }
}
