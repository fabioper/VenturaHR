using Common.Abstractions;
using Users.Api.Models.ValueObjects;

namespace Users.Api.Models.Entities;

public abstract class BaseUser : BaseEntity<UserId>, IAggregateRoot
{
    public string Name { get; protected set; }
    public string Email { get; protected set; }
}