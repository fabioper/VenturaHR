#nullable disable

using Common.Abstractions;
using Users.Infra.Data.Models.ValueObjects;

namespace Users.Infra.Data.Models.Entities;

public class Applicant : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Email { get; private set; }

    public Applicant(string id, string name, string email)
    {
        Id = Guid.Parse(id);
        Name = name;
        Email = email;
    }

    public Applicant() { }
}