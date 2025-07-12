using Domain.Common;

namespace Domain.Entities
{
    public class Membership:EntityBase<int>
    {
        public string Name { get; set; }               
        public string Description { get; set; }     
        public decimal Cost { get; set; }             
        public int Duration { get; set; }         // represent by month number
        public int Count { get; set; }            // number of trainees in this program
        public int GymId { get; set; }
        public ICollection<GymFeature> Features { get; set; } = new List<GymFeature>();
        public ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();
        public Gym Gym { get; set; }

        public int getCount()=> Trainees.Count;

    }
}
