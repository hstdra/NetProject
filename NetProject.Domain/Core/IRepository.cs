namespace NetProject.Domain.Core;

public interface IRepository<TAggregateRoot, in TId> where TAggregateRoot : AggregateRoot<TId>
{
    Task<bool> ExistsAsync(ISpecification<TAggregateRoot> specification, CancellationToken cancellationToken = default);

    Task<TAggregateRoot> FindOneAsync(ISpecification<TAggregateRoot> specification,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TAggregateRoot>> FindAllAsync(ISpecification<TAggregateRoot> specification,
        CancellationToken cancellationToken = default);

    Task AddAsync(TAggregateRoot aggregate, CancellationToken cancellationToken = default);

    Task SaveAsync(TAggregateRoot aggregate, CancellationToken cancellationToken = default);

    Task DeleteAsync(TAggregateRoot aggregate, CancellationToken cancellationToken = default);
}