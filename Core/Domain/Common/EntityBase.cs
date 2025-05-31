namespace Domain.Common;

public class EntityBase<TKey>
{
    public TKey Id { get; set; } = default!;
    public bool IsDeleted { get; set; }
}
