using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Gym: EntityBase<int>
    {
        public Address Address { get; set; }
        public GymType GymType { get; set; }
        public string Media { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public RequestStatus AddGymStatus { get; set; } = RequestStatus.Pending;
        public DeleteStatus DeleteGymStatus { get; set; } = DeleteStatus.NotRequested;
        public int GymOwnerId { get; set; }
        public GymOwner GymOwner { get; set; }
        public ICollection<GymFeature> GymFeatures { get; set; }=new List<GymFeature>();
        public ICollection<GymCoach> GymCoaches { get; set; } =new List<GymCoach>();
        public ICollection<Membership> Memberships { get; set; } =new List<Membership>();
        public ICollection<Class> Classes { get; set; } = new List<Class>();
        public ICollection<Trainee> Trainees { get; set; } = new List<Trainee>();

        public ICollection<Media>Images { get; set; }
    }
}
