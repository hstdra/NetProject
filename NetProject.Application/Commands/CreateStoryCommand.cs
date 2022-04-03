using NetProject.Domain.MemberAggregate;
using NetProject.Domain.StoryAggregate;
using NetProject.Infrastructure.Cqrs.Commands;

namespace NetProject.Application.Commands;

public record CreateStoryCommand(string Name, Guid CreatorId) : ICommand<Guid>;

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
        if (!validCreatorId) throw new ArgumentException($"Creator with id {command.CreatorId} does not exist");

        var story = new Story(command.Name, command.CreatorId);
        await _storyRepository.AddAsync(story, cancellationToken);

        return CommandResult<Guid>.Success(story.Id);
    }
}