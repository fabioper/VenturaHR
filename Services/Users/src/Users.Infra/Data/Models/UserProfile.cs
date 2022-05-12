#nullable disable

using Common;

namespace Users.Infra.Data.Models;

public class UserProfile : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string ExternalId { get; private set; }
    public List<string> Role { get; set; }

    public UserProfile() { }

    public UserProfile(string name, string email, string externalId)
    {
        Name = name;
        Email = email;
        ExternalId = externalId;
    }
}