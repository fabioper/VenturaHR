#nullable disable

namespace Users.Application.DTOs.Responses;

public record TokenResponse
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}