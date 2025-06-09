using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueObjects
{
    public class Schedule
    {
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
    }
}
