using Common.Abstractions;

namespace Users.Api.Models.Entities;

public abstract class BaseUser : Entity, IAggregateRoot
{
    public string Name { get; protected set; }
    public string Email { get; protected set; }
    public string ExternalId { get; protected set; }
}