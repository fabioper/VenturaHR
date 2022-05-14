namespace Users.Application.Models.Requests;

public class CreateCompanyProfileRequest
{
    public string Identifier { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Registration { get; set; }
    public string PhoneNumber { get; set; }
}