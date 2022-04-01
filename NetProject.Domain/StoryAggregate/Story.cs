using NetProject.Domain.Core;

namespace NetProject.Domain.StoryAggregate;

public class Story : AggregateRoot<Guid>
{
    public string Name { get; }
    public Guid CreatorId { get; }
    public List<Guid> OwnerIds { get; }

    public Story(string name, Guid creatorId)
    {
        Name = name;
        CreatorId = creatorId;
        OwnerIds = new List<Guid>();
    }

    public void AddOwner(Guid userId)
    {
        if (OwnerIds.Contains(userId)) return;
        OwnerIds.Add(userId);
    }

    public void RemoveOwner(Guid userId)
    {
        OwnerIds.Remove(userId);
    }
}