using NetProject.Domain.StoryAggregate;
using NetProject.Infrastructure.Database;

namespace NetProject.Infrastructure.Domain.Repositories;

public class StoryRepository : RepositoryBase<Story, Guid>, IStoryRepository
{
    public StoryRepository(AppDbContext context) : base(context)
    {
    }
}