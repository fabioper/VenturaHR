#nullable disable

using Common.Abstractions;

namespace Users.Api.Models.Entities;

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