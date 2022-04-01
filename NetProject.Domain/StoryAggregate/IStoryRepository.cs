using NetProject.Domain.Core;

namespace NetProject.Domain.StoryAggregate;

public interface IStoryRepository : IRepository<Story, Guid>
{
}