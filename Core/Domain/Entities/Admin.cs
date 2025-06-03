using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Admin:EntityBase<int>
    {
        public string Hamada { get; set; }
        public AppUser AppUser { get; set; }
        public string AppUserId { get; set; }
    }
}
