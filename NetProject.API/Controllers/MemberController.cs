using Microsoft.AspNetCore.Mvc;
using NetProject.Application.Commands;
using NetProject.Application.Queries;
using NetProject.Infrastructure.Cqrs.Commands;
using NetProject.Infrastructure.Cqrs.Queries;

namespace NetProject.API.Controllers;

[ApiController]
[Route("members")]
public class MemberController : ControllerBase
{
    private readonly IQueryBus _queryBus;
    private readonly ICommandBus _commandBus;

    public MemberController(IQueryBus queryBus, ICommandBus commandBus)
    {
        _queryBus = queryBus;
        _commandBus = commandBus;
    }

    [HttpGet]
    public async Task<IActionResult> GetMembers(CancellationToken cancellationToken)
    {
        var query = new GetAllMembersQuery();
        var result = await _queryBus.SendAsync(query, cancellationToken);

        return Ok(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetMemberById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetMemberByIdQuery(id);
        var result = await _queryBus.SendAsync(query, cancellationToken);
        if (result is null) return NotFound();
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMember([FromBody] CreateMemberCommand command, CancellationToken cancellationToken)
    {
        var result = await _commandBus.SendAsync(command, cancellationToken);

        return Ok(new {Id = result});
    }
}