namespace Users.Api.DTOs.Requests;

#nullable disable

public record CreateCompanyProfileRequest
{
    public string ExternalId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Registration { get; set; }
    public string PhoneNumber { get; set; }
}