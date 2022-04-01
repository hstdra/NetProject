namespace NetProject.Infrastructure.Cqrs.Commands;

public interface ICommandBus
{
    Task<TResponse> SendAsync<TResponse>(ICommand<TResponse> query, CancellationToken cancellationToken = default);
}