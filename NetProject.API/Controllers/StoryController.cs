using Microsoft.AspNetCore.Mvc;
using NetProject.API.Dtos;
using NetProject.Application.Commands;
using NetProject.Application.Queries;
using NetProject.Infrastructure.Cqrs.Commands;
using NetProject.Infrastructure.Cqrs.Queries;

namespace NetProject.API.Controllers;

[ApiController]
[Route("stories")]
public class StoryController : ControllerBase
{
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;

    public StoryController(IQueryBus queryBus, ICommandBus commandBus)
    {
        _queryBus = queryBus;
        _commandBus = commandBus;
    }

    [HttpGet]
    public async Task<IActionResult> GetStories(CancellationToken cancellationToken)
    {
        var query = new GetAllStoriesQuery();
        var result = await _queryBus.SendAsync(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Route("{storyId}")]
    public async Task<IActionResult> GetStoryById([FromRoute] Guid storyId, CancellationToken cancellationToken)
    {
        var query = new GetStoryByIdQuery {Id = storyId};
        var result = await _queryBus.SendAsync(query, cancellationToken);
        if (result is null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStory([FromBody] CreateStoryRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateStoryCommand
        {
            Name = request.Name,
            CreatorId = request.CreatorId,
        };
        var result = await _commandBus.SendAsync(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest();

        return Ok(new {Id = result.Response});
    }

    [HttpPost]
    [Route("{storyId}/owners")]
    public async Task<IActionResult> AddStoryOwner(
        [FromRoute] Guid storyId,
        [FromBody] AddStoryOwnerRequest request,
        CancellationToken cancellationToken)
    {
        var command = new AddStoryOwnerCommand
        {
            StoryId = storyId,
            OwnerId = request.OwnerId
        };
        var result = await _commandBus.SendAsync(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest();

        return Ok();
    }
    
    [HttpDelete]
    [Route("{storyId}/owners/{ownerId}")]
    public async Task<IActionResult> RemoveOwner(
        [FromRoute] Guid storyId,
        [FromRoute] Guid ownerId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveStoryOwnerCommand
        {
            StoryId = storyId,
            OwnerId = ownerId
        };
        var result = await _commandBus.SendAsync(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest();

        return Ok();
    }

    [HttpPost]
    [Route("{storyId}/tasks")]
    public async Task<IActionResult> AddStoryTask([FromRoute] Guid storyId, [FromBody] AddStoryTaskRequest taskRequest,
        CancellationToken cancellationToken)
    {
        var command = new AddStoryTaskCommand
        {
            StoryId = storyId,
            TaskName = taskRequest.Name
        };
        var result = await _commandBus.SendAsync(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest();

        return Ok();
    }

    [HttpPatch]
    [Route("{storyId}/tasks/{taskId}/is-done")]
    public async Task<IActionResult> RemoveStoryTask(
        [FromRoute] Guid storyId,
        [FromRoute] Guid taskId,
        [FromBody] ChangeStoryTaskIsDoneRequest request,
        CancellationToken cancellationToken)
    {
        var command = new ChangeStoryTaskIsDoneCommand()
        {
            StoryId = storyId,
            TaskId = taskId,
            IsDone = request.IsDone
        };
        var result = await _commandBus.SendAsync(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest();

        return Ok();
    }
    
    [HttpDelete]
    [Route("{storyId}/tasks/{taskId}")]
    public async Task<IActionResult> RemoveStoryTask(
        [FromRoute] Guid storyId,
        [FromRoute] Guid taskId,
        CancellationToken cancellationToken)
    {
        var command = new RemoveStoryTaskCommand
        {
            StoryId = storyId,
            StoryTaskId = taskId
        };
        var result = await _commandBus.SendAsync(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest();

        return Ok();
    }
}