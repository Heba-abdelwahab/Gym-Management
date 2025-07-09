namespace Domain.Entities.Chat;


public class Message
{
    public int Id { get; set; }

    public string SenderId { get; set; } = string.Empty;

    public string SenderUsername { get; set; } = string.Empty;

    public AppUser Sender { get; set; } = null!;

    public string RecipientId { get; set; } = string.Empty;

    public string RecipientUsername { get; set; } = string.Empty;

    public AppUser Recipient { get; set; } = null!;

    public string Content { get; set; } = string.Empty;

    public DateTime? DateRead { get; set; }

    public DateTime MessageSent { get; set; } = DateTime.UtcNow;
    public bool SenderDeleted { get; set; }
    public bool RecipientDeleted { get; set; }
}
