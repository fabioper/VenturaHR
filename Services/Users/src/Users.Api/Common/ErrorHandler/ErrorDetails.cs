using System.Net;

namespace Users.Api.Common.ErrorHandler;

public record ErrorDetails
{
    public HttpStatusCode Status { get; init; }
    public string Message { get; init; }
    public IDictionary<string, string>? Errors { get; init; }
}