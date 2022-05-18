#nullable disable

using Users.Api.Models.ValueObjects;

namespace Users.Api.Models.Entities;

public class Company : BaseUser
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }
    public Registration Registration { get; private set; }
    public string ExternalId { get; private set; }

    public Company(
        string name,
        string email,
        PhoneNumber phoneNumber,
        Registration registration,
        string externalId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        Registration = registration;
        ExternalId = externalId;
    }

    public Company() { }
}