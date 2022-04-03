using NetProject.Domain.Core;
using NetProject.Domain.StoryAggregate;
using NetProject.Infrastructure.Cqrs.Commands;

namespace NetProject.Application.Commands;

public record AddStoryTaskCommand(Guid StoryId, string TaskName) : ICommand;

public class AddStoryTaskCommandHandler : ICommandHandler<AddStoryTaskCommand>
{
    private readonly IStoryRepository _storyRepository;

    public AddStoryTaskCommandHandler(IStoryRepository storyRepository)
    {
        _storyRepository = storyRepository;
    }

    public async Task<CommandResult> Handle(AddStoryTaskCommand command, CancellationToken cancellationToken)
    {
        var story = await _storyRepository.FindOneAsync(command.StoryId, cancellationToken);
        if (story is null) return CommandResult.Error($"Story with id {command.StoryId} does not exist");

        story.AddStoryTask(command.TaskName);
        await _storyRepository.SaveAsync(story, cancellationToken);

        return CommandResult.Success();
    }
}