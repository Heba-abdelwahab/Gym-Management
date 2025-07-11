using Domain.Entities.Chat;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    //public DateOnly? DateOfBirth { get; set; }

    public ICollection<Message> MessagesSent { get; set; } = new HashSet<Message>();
    public ICollection<Message> MessagesReceived { get; set; } = new HashSet<Message>();
    public ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();
}
