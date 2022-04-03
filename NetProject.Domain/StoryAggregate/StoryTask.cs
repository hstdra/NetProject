using NetProject.Domain.Core;

namespace NetProject.Domain.StoryAggregate;

public class StoryTask : Entity<Guid>
{
    public Guid StoryId { get; private set; }
    public string Name { get; private set; }
    public bool IsDone { get; private set; }

    public StoryTask(Guid storyId, string name)
    {
        StoryId = storyId;
        Name = name;
    }

    public void ChangeIsDone(bool isDone)
    {
        IsDone = isDone;
    }
}