using NetProject.Domain.MemberAggregate;
using NetProject.Domain.StoryAggregate;
using NetProject.Infrastructure.Cqrs.Commands;

namespace NetProject.Application.Commands;

public class CreateStoryCommand : ICommand<Guid>
{
    public string Name { get; init; }
    public Guid CreatorId { get; init; }
}

public class CreateStoryCommandHandler : ICommandHandler<CreateStoryCommand, Guid>
{
    private readonly IStoryRepository _storyRepository;
    private readonly IMemberRepository _memberRepository;

    public CreateStoryCommandHandler(IStoryRepository storyRepository, IMemberRepository memberRepository)
    {
        _storyRepository = storyRepository;
        _memberRepository = memberRepository;
    }

    public async Task<CommandResult<Guid>> Handle(CreateStoryCommand command, CancellationToken cancellationToken)
    {
        var validCreatorId = await _memberRepository.ExistsAsync(command.CreatorId, cancellationToken);
        if (!validCreatorId) return CommandResult<Guid>.Error(".....");

        var story = new Story(command.Name, command.CreatorId);
        await _storyRepository.AddAsync(story, cancellationToken);

        return CommandResult<Guid>.Success(story.Id);
    }
}