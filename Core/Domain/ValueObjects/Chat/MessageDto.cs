namespace Domain.ValueObjects.Chat;

public record MessageDto
{
    public int Id { get; init; }

    public string SenderId { get; init; } = string.Empty;

    public string SenderUsername { get; init; } = string.Empty;
    public string SenderPhotoUrl { get; init; } = string.Empty;


    public string RecipientId { get; init; } = string.Empty;

    public string RecipientUsername { get; init; } = string.Empty;

    public string RecipientPhotoUrl { get; init; } = string.Empty;

    public string Content { get; init; } = string.Empty;

    public DateTime? DateRead { get; init; }

    public DateTime MessageSent { get; init; }

}