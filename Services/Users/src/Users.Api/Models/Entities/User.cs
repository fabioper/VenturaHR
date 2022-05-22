using Common.Abstractions;
using Users.Api.Models.ValueObjects;

namespace Users.Api.Models.Entities;

public class User : BaseEntity<UserId>, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Registration Registration { get; private set; }

    public User(
        UserId id,
        string name,
        string email,
        PhoneNumber phoneNumber,
        Registration registration)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Registration = registration;
    }
}