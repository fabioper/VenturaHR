namespace Users.Application.Models.Requests;

public class CreateApplicantProfileRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Identifier { get; set; }
}