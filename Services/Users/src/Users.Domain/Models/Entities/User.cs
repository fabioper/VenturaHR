#nullable disable

using Common.Abstractions;
using Common.Guards;
using Users.Domain.Models.Enums;
using Users.Domain.Models.ValueObjects;

namespace Users.Domain.Models.Entities;

public class User : BaseEntity, IAggregateRoot
{
    public string Name { get; }
    public string Email { get; }
    public string Password { get; }
    public PhoneNumber PhoneNumber { get; }
    public Registration Registration { get; }
    public UserType UserType { get; }

    public User(
        string name,
        string email,
        string password,
        PhoneNumber phoneNumber,
        Registration registration,
        UserType userType)
    {
        Guard.Against.NullOrEmpty(name, nameof(name));
        Guard.Against.NullOrEmpty(email, nameof(email));
        Guard.Against.NullOrEmpty(password, nameof(password));

        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
        Registration = registration;
        UserType = userType;
    }

    public User() { } // Ef required
}