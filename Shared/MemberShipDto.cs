using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Shared
{
    public class MemberShipDto
    {
        public int Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public int Duration { get; set; }    
        public int GymId { get; set; }
        public int Count { get; set; }
        public ICollection<GymFeatureReturnDto> Features { get; set; }


    }
}
