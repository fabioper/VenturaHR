#nullable disable

using Common;
using Users.Infra.Data.Models.ValueObjects;

namespace Users.Infra.Data.Models.Entities;

public class Applicant : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }
    public ExternalId ExternalId { get; private set; }

    public Applicant(string name, string email, ExternalId externalId)
    {
        Name = name;
        Email = email;
        ExternalId = externalId;
    }

    public Applicant() { }
}