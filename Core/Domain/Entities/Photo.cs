using Domain.Common;

namespace Domain.Entities;

public class Photo : EntityBase<int>
{
    public string Url { get; set; } = string.Empty;
    public bool IsMain { get; set; } = false;
    public string PublicId { get; set; } = string.Empty;
    public string AppUserId { get; set; } = string.Empty;

}