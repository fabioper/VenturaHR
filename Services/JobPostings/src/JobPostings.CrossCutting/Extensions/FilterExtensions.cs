using JobPostings.CrossCutting.Filters;

namespace JobPostings.CrossCutting.Extensions;

public static class FilterExtensions
{
    public static FilterResponse<T> ToFilterResponse<T>(this List<T> collection, BaseFilter filter, int total)
    {
        return new FilterResponse<T>
        {
            Page = filter.Page,
            Results = collection,
            Total = total
        };
    }

    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, BaseFilter filter)
    {
        var paginatedQuery = queryable
            .Skip(filter.PageSize * (filter.Page - 1))
            .Take(filter.PageSize);

        return paginatedQuery;
    }
}