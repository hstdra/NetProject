using System.Linq.Expressions;

namespace NetProject.Domain.Core;

public class SpecificationBase<T> : ISpecification<T>
{
    public Expression<Func<T, bool>> Expression { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();

    public SpecificationBase(Expression<Func<T, bool>> expression)
    {
        Expression = expression;
    }

    protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
}