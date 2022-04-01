using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetProject.Domain.Core;

namespace NetProject.Infrastructure.Cqrs.Commands;

public class CommandBus : ICommandBus
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;

    public CommandBus(IServiceProvider serviceProvider)
    {
        _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        _mediator = serviceProvider.GetRequiredService<IMediator>();
    }

    public Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> query,
        CancellationToken cancellationToken = default)
    {
        return _unitOfWork.ExecuteAsync(() => _mediator.Send(query, cancellationToken), cancellationToken);
    }
}