#nullable disable

using Common.Abstractions;

namespace Users.Api.Models.Entities;

public class Applicant : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string ExternalId { get; private set; }

    public Applicant(
        string name,
        string email,
        string externalId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        ExternalId = externalId;
    }

    public Applicant(string externalId) => ExternalId = externalId;
}