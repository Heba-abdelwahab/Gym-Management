using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class MemberShipReturnDto
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Duration { get; set; }       
        public int Count { get; set; }         
        public int GymId { get; set; }


        public List<int>GymFeaturesId { get; set; }


    }
}
