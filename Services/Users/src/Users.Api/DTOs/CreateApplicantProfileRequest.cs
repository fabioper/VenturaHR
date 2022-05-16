namespace Users.Api.DTOs;

#nullable disable

public record CreateApplicantProfileRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Identifier { get; set; }
}