namespace Users.Api.Models.Requests;

public class FinishUserProfileDto
{
    public string ExternalId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<string> Role { get; set; }
}