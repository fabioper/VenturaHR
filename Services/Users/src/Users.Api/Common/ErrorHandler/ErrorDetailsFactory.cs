using System.Net;
using Common.Exceptions;
using Users.CrossCutting.Exceptions;

namespace Users.Api.Common.ErrorHandler;

public static class ErrorDetailsFactory
{
    public static ErrorDetails Create(Exception exception) => exception switch
    {
        ValidationException e => new ErrorDetails
        {
          Message = e.Message,
          Status = HttpStatusCode.BadRequest,
          Errors = e.Errors,
        },
        InvalidRefreshTokenException e => new ErrorDetails
        {
            Message = e.Message,
            Status = HttpStatusCode.Forbidden,
        },
        UnrecognizedUserTypeException e => new ErrorDetails
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
        { } e => new ErrorDetails
        {
            Message = e.Message,
            Status = HttpStatusCode.InternalServerError,
        },
    };
}