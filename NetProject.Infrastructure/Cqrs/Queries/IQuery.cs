using MediatR;

namespace NetProject.Infrastructure.Cqrs.Queries;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}