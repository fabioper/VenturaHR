namespace Webhooks.Models.DTO;

public class UserCreatedDto
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Email { get; set; }
    public List<string> Role { get; set; }
}