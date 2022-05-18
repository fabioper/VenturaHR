namespace Users.Api.DTOs.Requests;

#nullable disable

public record CreateApplicantProfileRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string ExternalId { get; set; }
}