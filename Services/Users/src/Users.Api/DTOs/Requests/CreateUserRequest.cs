using Users.Api.Models.Enums;

namespace Users.Api.DTOs.Requests;

#nullable disable

public record CreateUserRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Registration { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public UserType UserType { get; set; }
}