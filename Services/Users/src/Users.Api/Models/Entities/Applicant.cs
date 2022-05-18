#nullable disable

namespace Users.Api.Models.Entities;

public class Applicant : BaseUser
{
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