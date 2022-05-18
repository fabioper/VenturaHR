using System.Net;
using Common.Exceptions;

namespace Users.Api.Common.ErrorHandler;

public static class ErrorDetailsFactory
{
    public static ErrorDetails Create(Exception exception) => exception switch
    {
        EntityNotFoundException e => new ErrorDetails
        {
            Message = e.Message,
            Status = HttpStatusCode.NotFound
        },
        _ => new ErrorDetails()
    };
}