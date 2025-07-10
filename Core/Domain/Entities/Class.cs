using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Class: EntityBase<int>
    {
        public string Name { get; set; }               
        public string Description { get; set; }   
        public DateTime Date { get; set; }
        //public Schedule Schedule { get; set; }        
        public decimal Cost { get; set; }             
        public int Capacity { get; set; }            
        public int CurrentCapacity { get; set; } 
        public int GymId { get; set; }
        public int? CoachId { get; set; }
        public Gym? Gym { get; set; }
        public Coach? Coach { get; set; }
        public ICollection<Trainee>? Trainees { get; set; } = new List<Trainee>();
    }
}
