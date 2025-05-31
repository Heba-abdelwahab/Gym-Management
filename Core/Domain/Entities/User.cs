using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User:EntityBase<int>
    {
        public string? name { get; set; }
        public string? address { get; set; }
    }
}
