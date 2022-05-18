namespace Users.Api.DTOs.Responses;

public record ApplicantProfileResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string ExternalId { get; init; }
}