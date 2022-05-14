namespace Users.Application.Models.Requests;

#nullable disable

public record CreateCompanyProfileRequest
{
    public string Identifier { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Registration { get; set; }
    public string PhoneNumber { get; set; }
}