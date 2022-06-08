using JobPostings.CrossCutting.Filters;

namespace JobPostings.CrossCutting.Extensions;

public static class FilterResponseExtensions
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
}