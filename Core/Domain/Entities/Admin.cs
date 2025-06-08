using Domain.Common;

namespace Domain.Entities
{
    public class Admin : EntityBase<int>
    {
        public string Hamada { get; set; } = string.Empty;
        public string AppUserId { get; set; } = string.Empty;
        public AppUser AppUser { get; set; } = null!;
    }
}
