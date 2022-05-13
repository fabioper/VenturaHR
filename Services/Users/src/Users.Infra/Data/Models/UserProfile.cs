#nullable disable

using Common;

namespace Users.Infra.Data.Models;

public class UserProfile : Entity, IAggregateRoot
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string ExternalId { get; set; }
    public string Role { get; set; }
}