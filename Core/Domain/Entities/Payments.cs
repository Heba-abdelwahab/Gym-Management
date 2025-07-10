using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Payments : EntityBase<int>
    {
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string? PaymentIntentId { get; set; } // Unique identifier for the intent of the payment
        public string? ClientSecret { get; set; }
        public PayFor PayFor { get; set; } // Enum to specify what the payment is for (Membership, Class, Feature)
        public int TraineeId { get; set; } // Foreign key to Trainee
        public Trainee Trainee { get; set; } // Navigation property to Trainee
    }

    public enum PayFor
    {
        Membership,
        Class,
        Feature
    }
}
