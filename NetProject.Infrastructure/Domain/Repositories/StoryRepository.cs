using NetProject.Domain.Core;
using NetProject.Domain.StoryAggregate;
using NetProject.Infrastructure.Database;

namespace NetProject.Infrastructure.Domain.Repositories;

public class StoryRepository : RepositoryBase<Story, Guid>, IStoryRepository
{
    public StoryRepository(AppDbContext context) : base(context)
    {
    }

    Task<Story> IRepository<Story, Guid>.FindOneAsync(Guid id, CancellationToken cancellationToken)
    {
        var spec = new SpecificationBase<Story>(x => x.Id == id);
        spec.Includes.Add(x => x.StoryTasks);
        return FindOneAsync(spec, cancellationToken);
    }
}