using System.Net;

namespace JobPostings.Api.Common.ErrorHandler;

public record ErrorDetails
{
    public ErrorDetails() { }
    public ErrorDetails(IDictionary<string, string> errors, HttpStatusCode status)
    {
        Errors = errors;
        Status = status;
    }

    private const string DefaultErrorMessage = "Ocorreu um erro";
    
    public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;
    public string Message { get; set; } = DefaultErrorMessage;
    public IDictionary<string, string>? Errors { get; }
}