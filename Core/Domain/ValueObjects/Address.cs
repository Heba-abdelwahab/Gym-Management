using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Address
    {
        public string Street {  get; set; }
        public string City {  get; set; }
        public string Country {  get; set; }
        public Location? Location { get; set; }

    }
}
