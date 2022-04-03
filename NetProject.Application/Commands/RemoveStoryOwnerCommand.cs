using NetProject.Domain.StoryAggregate;
using NetProject.Infrastructure.Cqrs.Commands;

namespace NetProject.Application.Commands;

public class RemoveStoryOwnerCommand : ICommand
{
    public Guid StoryId { get; init; }
    public Guid OwnerId { get; init; }
}

public class RemoveStoryOwnerCommandHandler : ICommandHandler<RemoveStoryOwnerCommand>
{
    private readonly IStoryRepository _storyRepository;

    public RemoveStoryOwnerCommandHandler(IStoryRepository storyRepository)
    {
        _storyRepository = storyRepository;
    }

    public async Task<CommandResult> Handle(RemoveStoryOwnerCommand command, CancellationToken cancellationToken)
    {
        var story = await _storyRepository.FindOneAsync(command.StoryId, cancellationToken);
        if (story is null) return CommandResult.Error($"Story with id {command.StoryId} does not exist");

        story.RemoveOwner(command.OwnerId);
        await _storyRepository.SaveAsync(story, cancellationToken);

        return CommandResult.Success();
    }
}