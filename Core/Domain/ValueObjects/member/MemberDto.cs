namespace Domain.ValueObjects.member;

public class MemberDto
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;

    public string PhotoUrl { get; set; } = string.Empty;

    public string KnownAs { get; set; } = string.Empty;
    public DateTime LastActive { get; set; }

    public IReadOnlyList<PhotoDto> Photos { get; set; } = null!;



}