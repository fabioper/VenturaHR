using System.Net;
using Common.Exceptions;
using Users.Application.Exceptions;

namespace Users.Api.Common.ErrorHandler;

public static class ErrorDetailsFactory
{
    public static ErrorDetails Create(Exception exception) => exception switch
    {
        InvalidRefreshToken e => new ErrorDetails
        {
            Message = e.Message,
            Status = HttpStatusCode.Forbidden,
        },
        UnrecognizedUserType e => new ErrorDetails
        {
            Message = e.Message,
            Status = HttpStatusCode.BadRequest,
        },
        InvalidCredentialException e => new ErrorDetails
        {
            Message = e.Message,
            Status = HttpStatusCode.Unauthorized,
        },
        EntityNotFoundException e => new ErrorDetails
        {
            Message = e.Message,
            Status = HttpStatusCode.NotFound,
        },
        _ => new ErrorDetails(),
    };
}