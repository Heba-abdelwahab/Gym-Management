using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities
{
    namespace Domain.Entities
    {
        public class Media : EntityBase<int>
        {
            public MediaValueObj MediaValue { get; set; }
            public int? gymId { get; set; }
            public int? CoachId { get; set; }
            //public int? featureId { get; set; }
            public Gym? gym { get; set; }
            //public Feature? feature { get; set; }
            public Coach? Coach { get; set; }

        }
    }
}
