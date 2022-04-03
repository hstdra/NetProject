using NetProject.Domain.StoryAggregate;
using NetProject.Infrastructure.Cqrs.Commands;

namespace NetProject.Application.Commands;

public record RemoveStoryTaskCommand(Guid StoryId, Guid StoryTaskId) : ICommand;

public class RemoveStoryTaskCommandHandler : ICommandHandler<RemoveStoryTaskCommand>
{
    private readonly IStoryRepository _storyRepository;

    public RemoveStoryTaskCommandHandler(IStoryRepository storyRepository)
    {
        _storyRepository = storyRepository;
    }

    public async Task<CommandResult> Handle(RemoveStoryTaskCommand command, CancellationToken cancellationToken)
    {
        var story = await _storyRepository.FindOneAsync(command.StoryId, cancellationToken);
        if (story is null) return CommandResult.Error($"Story with id {command.StoryId} does not exist");

        story.RemoveStoryTask(command.StoryTaskId);
        await _storyRepository.SaveAsync(story, cancellationToken);

        return CommandResult.Success();
    }
}