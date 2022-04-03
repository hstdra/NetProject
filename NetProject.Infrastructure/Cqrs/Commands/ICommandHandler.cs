using MediatR;

namespace NetProject.Infrastructure.Cqrs.Commands;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, CommandResult<TResponse>>
    where TCommand : IRequest<CommandResult<TResponse>>
{
}

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, CommandResult>
    where TCommand : IRequest<CommandResult>
{
}