using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Chat;

public class Group
{
    [Key]
    public string Name { get; set; } = string.Empty;
    public ICollection<Connection> Connections { get; set; } = new HashSet<Connection>();
}
