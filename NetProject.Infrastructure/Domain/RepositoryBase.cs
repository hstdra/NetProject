using Microsoft.EntityFrameworkCore;
using NetProject.Domain.Core;

namespace NetProject.Infrastructure.Domain;

public abstract class RepositoryBase<TAggregateRoot, TId> : IRepository<TAggregateRoot, TId>
    where TAggregateRoot : AggregateRoot<TId>
{
    protected DbContext DbContext { get; }

    protected RepositoryBase(DbContext context)
    {
        DbContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Task<bool> ExistsAsync(ISpecification<TAggregateRoot> specification,
        CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TAggregateRoot>().AnyAsync(specification.Expression, cancellationToken);
    }

    public Task<TAggregateRoot> FindOneAsync(ISpecification<TAggregateRoot> specification,
        CancellationToken cancellationToken = default)
    {
        return DbContext.Set<TAggregateRoot>().FirstOrDefaultAsync(specification.Expression, cancellationToken);
    }

    public Task<IEnumerable<TAggregateRoot>> FindAllAsync(ISpecification<TAggregateRoot> specification,
        CancellationToken cancellationToken = default)
    {
        var queryable = DbContext.Set<TAggregateRoot>().AsQueryable();
        if (specification == null)
        {
            var result = queryable
                .AsNoTracking()
                .AsEnumerable();
            return Task.FromResult(result);
        }
        else
        {
            var queryableWithInclude = specification.Includes
                .Aggregate(queryable, (current, include) => current.Include(include));
            var result = queryableWithInclude
                .Where(specification.Expression)
                .AsNoTracking()
                .AsEnumerable();
            return Task.FromResult(result);
        }
    }

    public async Task AddAsync(TAggregateRoot aggregate, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<TAggregateRoot>().AddAsync(aggregate, cancellationToken);
    }

    public Task SaveAsync(TAggregateRoot aggregate, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TAggregateRoot>().Update(aggregate);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TAggregateRoot aggregate, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TAggregateRoot>().Remove(aggregate);
        return Task.CompletedTask;
    }
}