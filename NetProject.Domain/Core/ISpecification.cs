﻿using System.Linq.Expressions;

namespace NetProject.Domain.Core;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> Expression { get; }
    List<Expression<Func<T, object>>> Includes { get; }
}