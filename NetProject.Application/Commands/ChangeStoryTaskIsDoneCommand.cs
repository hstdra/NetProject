using NetProject.Domain.StoryAggregate;
using NetProject.Infrastructure.Cqrs.Commands;

namespace NetProject.Application.Commands;

public class ChangeStoryTaskIsDoneCommand : ICommand
{
    public Guid StoryId { get; init; }
    public Guid TaskId { get; init; }
    public bool IsDone { get; set; }
}

public class ChangeStoryTaskIsDoneCommandHandler : ICommandHandler<ChangeStoryTaskIsDoneCommand>
{
    private readonly IStoryRepository _storyRepository;

    public ChangeStoryTaskIsDoneCommandHandler(IStoryRepository storyRepository)
    {
        _storyRepository = storyRepository;
    }

    public async Task<CommandResult> Handle(ChangeStoryTaskIsDoneCommand command, CancellationToken cancellationToken)
    {
        var story = await _storyRepository.FindOneAsync(command.StoryId, cancellationToken);
        if (story is null) return CommandResult.Error($"Story with id {command.StoryId} does not exist");

        var task = story.StoryTasks.FirstOrDefault(x => x.Id == command.TaskId);
        if (task is null) return CommandResult.Error($"Task with id {command.TaskId} does not exist");
        
        task.ChangeIsDone(command.IsDone);
        await _storyRepository.SaveAsync(story, cancellationToken);

        return CommandResult.Success();
    }
}