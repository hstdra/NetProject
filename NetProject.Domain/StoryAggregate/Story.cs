using NetProject.Domain.Core;

namespace NetProject.Domain.StoryAggregate;

public class Story : AggregateRoot<Guid>
{
    public string Name { get; }
    public Guid CreatorId { get; }
    public List<Guid> OwnerIds { get; }

    public List<StoryTask> StoryTasks { get; }

    public Story(string name, Guid creatorId)
    {
        Name = name;
        CreatorId = creatorId;
        OwnerIds = new List<Guid>();
        StoryTasks = new List<StoryTask>();
    }

    public void AddOwner(Guid ownerId)
    {
        if (OwnerIds.Contains(ownerId)) return;
        OwnerIds.Add(ownerId);
    }

    public void RemoveOwner(Guid ownerId)
    {
        OwnerIds.Remove(ownerId);
    }

    public void AddStoryTask(string taskName)
    {
        StoryTasks.Add(new StoryTask(Id, taskName));
    }
    
    public void RemoveStoryTask(Guid storyTaskId)
    {
        var current = StoryTasks.FirstOrDefault(x => x.Id == storyTaskId);
        StoryTasks.Remove(current);
    }
    
    public void ChangeStoryTaskIsDone(Guid storyTaskId, bool isDone)
    {
        var current = StoryTasks.FirstOrDefault(x => x.Id == storyTaskId);
        current?.ChangeIsDone(isDone);
    }
}