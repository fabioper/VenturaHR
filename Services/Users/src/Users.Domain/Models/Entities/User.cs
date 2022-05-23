#nullable disable

using Common.Abstractions;
using Common.Guards;
using Users.Domain.Models.Enums;
using Users.Domain.Models.ValueObjects;

namespace Users.Domain.Models.Entities;

public class User : BaseEntity<UserId>, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Registration Registration { get; private set; }
    public UserType UserType { get; private set; }

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

        Id = new UserId();
        Name = name;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
        Registration = registration;
        UserType = userType;
    }

    public User() { } // Ef required
}