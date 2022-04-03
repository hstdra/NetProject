using NetProject.Domain.Core;
using NetProject.Domain.MemberAggregate;
using NetProject.Domain.StoryAggregate;
using NetProject.Infrastructure.Cqrs.Commands;

namespace NetProject.Application.Commands;

public record AddStoryOwnerCommand(Guid StoryId, Guid OwnerId) : ICommand;

public class AddStoryOwnerCommandHandler : ICommandHandler<AddStoryOwnerCommand>
{
    private readonly IStoryRepository _storyRepository;
    private readonly IMemberRepository _memberRepository;

    public AddStoryOwnerCommandHandler(IStoryRepository storyRepository, IMemberRepository memberRepository)
    {
        _storyRepository = storyRepository;
        _memberRepository = memberRepository;
    }

    public async Task<CommandResult> Handle(AddStoryOwnerCommand command, CancellationToken cancellationToken)
    {
        var story = await _storyRepository.FindOneAsync(command.StoryId, cancellationToken);
        if (story is null) return CommandResult.Error($"Story with id {command.StoryId} does not exist");

        var validOwnerId = await _memberRepository.ExistsAsync(command.OwnerId, cancellationToken);
        if (!validOwnerId) return CommandResult.Error($"Owner with id {command.OwnerId} does not exist");

        story.AddOwner(command.OwnerId);
        await _storyRepository.SaveAsync(story, cancellationToken);

        return CommandResult.Success();
    }
}