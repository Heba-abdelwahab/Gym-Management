using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WorkDay:EntityBase<int>
    {
        public TimeOnly Start {  get; set; }
        public TimeOnly End { get; set; }
        public DateTime Day {  get; set; }
        public int GymCoachId { get; set; }
        public GymCoach GymCoach {  get; set; }
    }
}
