#nullable disable

using Users.Api.Models.ValueObjects;

namespace Users.Api.Models.Entities;

public class Applicant : BaseUser
{
    public Applicant(string name, string email, UserId applicantId)
    {
        Id = applicantId;
        Name = name;
        Email = email;
    }

    public Applicant() { }
}