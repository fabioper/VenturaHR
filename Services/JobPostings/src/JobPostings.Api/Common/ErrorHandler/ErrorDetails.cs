using System.Net;

namespace JobPostings.Api.Common.ErrorHandler;

public record ErrorDetails
{
    private const string DefaultErrorMessage = "Ocorreu um erro";
    
    public HttpStatusCode Status { get; set; } = HttpStatusCode.InternalServerError;
    public string Message { get; set; } = DefaultErrorMessage;
    public IDictionary<string, string>? Errors { get; } = new Dictionary<string, string>();
}