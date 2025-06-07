using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WorkDay : EntityBase<int>
    {
        public TimeOnly Start {  get; set; }
        public TimeOnly End { get; set; }
        public DayOfWeek Day {  get; set; }


        public int GymCoachId { get; set; }
        public GymCoach? GymCoach { get; set; }
    }
}
