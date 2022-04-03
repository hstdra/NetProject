using NetProject.Domain.StoryAggregate;
using NetProject.Infrastructure.Cqrs.Queries;

namespace NetProject.Application.Queries;

public class GetStoryByIdQuery : IQuery<Story>
{
    public Guid Id { get; init; }
}

public class GetStoryByIdQueryHandler : IQueryHandler<GetStoryByIdQuery, Story>
{
    private readonly IStoryRepository _memberRepository;

    public GetStoryByIdQueryHandler(IStoryRepository memberRepository)
    {
        _memberRepository = memberRepository;
    }

    public Task<Story> Handle(GetStoryByIdQuery request, CancellationToken cancellationToken)
    {
        return _memberRepository.FindOneAsync(request.Id, cancellationToken);
    }
}