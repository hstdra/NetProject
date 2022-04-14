using NetProject.Domain.Core;

namespace NetProject.Domain.StoryAggregate;

public class StoryTask : Entity<Guid>
{
    public Guid StoryId { get; }
    public string Name { get; }
    public bool IsDone { get; private set; }

    internal StoryTask(Guid storyId, string name)
    {
        StoryId = storyId;
        Name = name;
    }

    internal void ChangeIsDone(bool isDone)
    {
        IsDone = isDone;
    }
}