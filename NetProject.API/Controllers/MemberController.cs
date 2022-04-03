using Microsoft.AspNetCore.Mvc;
using NetProject.API.Dtos;
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
    [Route("{memberId}")]
    public async Task<IActionResult> GetMemberById([FromRoute] Guid memberId, CancellationToken cancellationToken)
    {
        var query = new GetMemberByIdQuery {Id = memberId};
        var result = await _queryBus.SendAsync(query, cancellationToken);
        if (result is null) return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMember([FromBody] CreateMemberRequest request,
        CancellationToken cancellationToken)
    {
        var command = new CreateMemberCommand
        {
            Name = request.Name,
            Username = request.Username
        };
        var result = await _commandBus.SendAsync(command, cancellationToken);
        if (!result.IsSuccess) return BadRequest();

        return Ok(new {Id = result.Response});
    }
}