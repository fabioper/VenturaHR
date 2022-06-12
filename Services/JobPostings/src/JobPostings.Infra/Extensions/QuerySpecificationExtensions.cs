using Common.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Infra.Extensions;

public static class QuerySpecificationExtensions
{
    public static IQueryable<T> Specify<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class
    {
        var queryableResultWithIncludes = spec.Includes
            .Aggregate(query,
                (current, include) => current.Include(include));
 
        var secondaryResult = spec.IncludeStrings
            .Aggregate(queryableResultWithIncludes,
                (current, include) => current.Include(include));
 
        return secondaryResult.Where(spec.Criteria);
    }
}