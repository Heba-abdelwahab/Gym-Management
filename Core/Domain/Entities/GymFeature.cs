using Domain.Common;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GymFeature:EntityBase<int>
    {
        public MediaValueObj Image { get; set; }         
        public string Description { get; set; }   
        public decimal Cost { get; set; }
        public int GymId { get; set; }  //foreign key
        public int FeatureId { get; set; } //foreign key
        public Gym Gym { get; set; }
        public Feature Feature { get; set; }
        public ICollection<TraineeSelectedFeature> TraineeSelectedFeatures { get; set; }=new HashSet<TraineeSelectedFeature>();
        public ICollection<Membership> Memberships { get; set; } = new HashSet<Membership>();
    }
}
