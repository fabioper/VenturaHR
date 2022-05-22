#nullable disable

using Common.Abstractions;
using Users.Api.Models.Enums;
using Users.Api.Models.ValueObjects;

namespace Users.Api.Models.Entities;

public class User : BaseEntity<UserId>, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Registration Registration { get; private set; }
    public UserType UserType { get; private set; }

    public User(
        UserId id,
        string name,
        string email,
        string password,
        PhoneNumber phoneNumber,
        Registration registration,
        UserType userType)
    {
        Id = id;
        Name = name;
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
        Registration = registration;
        UserType = userType;
    }

    public User() { } // Ef required
}