namespace NetProject.Domain.Core;

public abstract class Entity<TId>
{
    public TId Id { get; set; }
}