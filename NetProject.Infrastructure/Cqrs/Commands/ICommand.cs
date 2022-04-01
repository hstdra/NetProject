using MediatR;

namespace NetProject.Infrastructure.Cqrs.Commands;

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}