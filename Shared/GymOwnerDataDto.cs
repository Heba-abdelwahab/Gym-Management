using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class GymOwnerDataDto
    {
   public  string GymType{ get; set; }
        public string Name { get; set; }
        public int  MembershipsCount { get; set; }
        public int CoachesCount { get; set; }
        public int FeaturesCount { get; set; }
        public int TraineesCount { get; set; }
        public int ClassesCount { get; set; }
        public GymOwnerDataDto() { }


    }
}
