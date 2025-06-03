using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Muscle:EntityBase<int>
    {
        public string Name { get; set; }
        public ICollection<MuscleExerices> MuscleExerices { get; set; } = new List<MuscleExerices>();
    }
}
