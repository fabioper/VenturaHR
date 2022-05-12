namespace Common.Events.Models;

#nullable disable

public class UserCreated
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string ExternalId { get; set; }
    public List<string> Role { get; set; }

    public UserCreated(
        string name,
        string email,
        string externalId,
        List<string> role)
    {
        Name = name;
        Email = email;
        ExternalId = externalId;
        Role = role;
    }
}