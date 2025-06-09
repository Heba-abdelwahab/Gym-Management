using Domain.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GymOwner:EntityBase<int>
    {
       public Collection<Gym>Gyms { get; set; } = new Collection<Gym>();
        public string AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}
