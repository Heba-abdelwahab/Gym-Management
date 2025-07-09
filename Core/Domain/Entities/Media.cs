using Domain.Common;
using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Media : EntityBase<int>
    {
        public MediaType Type { get; set; } = MediaType.Image;
        public string Url { get; set; } = string.Empty;
        public int PublicId { get; set; }
        public bool IsMain { get; set; } = false;

    }
}
