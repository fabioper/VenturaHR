namespace Users.Api.DTOs.Requests;

public record LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}