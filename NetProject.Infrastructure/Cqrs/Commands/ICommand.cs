using MediatR;

namespace NetProject.Infrastructure.Cqrs.Commands;

public interface ICommand : IRequest<CommandResult>
{
}

public interface ICommand<TResponse> : IRequest<CommandResult<TResponse>>
{
}