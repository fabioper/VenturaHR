using System.Net;
using Common.Exceptions;
using JobPostings.Domain.Exceptions;

namespace JobPostings.Api.Common.ErrorHandler;

public static class ErrorDetailsFactory
{
    public static ErrorDetails Create(Exception exception) => exception switch
    {
        JobPostingAlreadyAppliedException e => new ErrorDetails
        {
            Message = e.Message,
            Status = HttpStatusCode.BadRequest
        },
        ExpiredJobPostingException e => new ErrorDetails
        {
            Message = e.Message,
            Status = HttpStatusCode.BadRequest
        },
        EntityNotFoundException e => new ErrorDetails
        {
            Message = e.Message,
            Status = HttpStatusCode.NotFound
        },
        _ => new ErrorDetails(),
    };
}