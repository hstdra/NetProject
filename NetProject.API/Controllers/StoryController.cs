using Microsoft.AspNetCore.Mvc;
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
    [Route("{id}")]
    public async Task<IActionResult> GetStoryById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetStoryByIdQuery(id);
        var result = await _queryBus.SendAsync(query, cancellationToken);
        if (result is null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateStory([FromBody] CreateStoryCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _commandBus.SendAsync(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest();

        return Ok(new {Id = result.Response});
    }

    [HttpPost]
    [Route("{id}/owners")]
    public async Task<IActionResult> AddStoryOwner([FromBody] AddStoryOwnerCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _commandBus.SendAsync(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest();

        return Ok();
    }

    [HttpPost]
    [Route("{id}/tasks")]
    public async Task<IActionResult> AddStoryTask([FromBody] AddStoryTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _commandBus.SendAsync(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest();

        return Ok();
    }

    [HttpDelete]
    [Route("{id}/tasks/{taskId}")]
    public async Task<IActionResult> RemoveStoryTask([FromBody] RemoveStoryTaskCommand command,
        CancellationToken cancellationToken)
    {
        var result = await _commandBus.SendAsync(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest();

        return Ok();
    }
}