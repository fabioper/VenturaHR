using Common;
using Users.Infra.Data.Models.ValueObjects;

#nullable disable

namespace Users.Infra.Data.Models.Entities;

public class Company : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Registration Registration { get; private set; }
    public ExternalId ExternalId { get; private set; }

    public Company(
        string name,
        string email,
        PhoneNumber phoneNumber,
        Registration registration,
        ExternalId externalId)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Registration = registration;
        ExternalId = externalId;
    }

    public Company() { }
}