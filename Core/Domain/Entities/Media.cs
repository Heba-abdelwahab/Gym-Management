using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Media:EntityBase<int>
    {
        public MediaType Type { get; set; }
        public string Url { get; set; }
        public int PublicId { get; set; }
        public bool IsMain { get; set; } = false;

        public int? gymId { get; set; }
        public int? featureId { get; set; }

        public Gym? gym { get; set; }
        public Feature? feature { get; set; }

    }
}
