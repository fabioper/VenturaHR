#nullable disable

using Common.Abstractions;
using Users.Infra.Data.Models.ValueObjects;

namespace Users.Infra.Data.Models.Entities;

public class Company : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Registration Registration { get; private set; }

    public Company(
        string id,
        string name,
        string email,
        PhoneNumber phoneNumber,
        Registration registration)
    {
        Id = Guid.Parse(id);
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Registration = registration;
    }

    public Company() { }
}