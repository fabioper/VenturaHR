namespace Users.Api.DTOs.Requests;

#nullable disable

public record LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}