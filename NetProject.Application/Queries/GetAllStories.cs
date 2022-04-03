using NetProject.Domain.StoryAggregate;
using NetProject.Infrastructure.Cqrs.Queries;

namespace NetProject.Application.Queries;

public class GetAllStoriesQuery : IQuery<IEnumerable<Story>>
{
}

public class GetAllStoriesQueryHandler : IQueryHandler<GetAllStoriesQuery, IEnumerable<Story>>
{
    private readonly IStoryRepository _storyRepository;

    public GetAllStoriesQueryHandler(IStoryRepository storyRepository)
    {
        _storyRepository = storyRepository;
    }

    public Task<IEnumerable<Story>> Handle(GetAllStoriesQuery request, CancellationToken cancellationToken)
    {
        return _storyRepository.FindAllAsync(null, cancellationToken);
    }
}