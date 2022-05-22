namespace Users.Api.DTOs.Responses;

public record UserProfileResponse
{
    public Guid Id { get; set; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Registration { get; init; }
    public string ExternalId { get; init; }
}