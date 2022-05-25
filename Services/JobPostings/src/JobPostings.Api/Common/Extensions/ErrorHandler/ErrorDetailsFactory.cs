using System.Net;
using Common.Exceptions;

namespace JobPostings.Api.Common.Extensions.ErrorHandler;

public static class ErrorDetailsFactory
{
    public static ErrorDetails Create(Exception exception) => exception switch
    {
        EntityNotFoundException e => new ErrorDetails
        {
            Message = e.Message,
            Status = HttpStatusCode.NotFound,
        },
        _ => new ErrorDetails(),
    };
}