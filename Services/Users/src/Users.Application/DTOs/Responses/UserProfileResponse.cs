using Users.Domain.Models.Enums;

namespace Users.Application.DTOs.Responses;

#nullable disable

public record UserProfileResponse
{
    public Guid Id { get; set; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Registration { get; init; }
    public UserType UserType { get; set; }
}