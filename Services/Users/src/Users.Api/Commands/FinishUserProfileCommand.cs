using MediatR;

namespace Users.Api.Commands;

public class FinishUserProfileCommand : IRequest
{
    public string ExternalId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public List<string> Role { get; set; }

    public FinishUserProfileCommand(
        string externalId,
        string name,
        string email,
        List<string> role)
    {
        ExternalId = externalId;
        Name = name;
        Email = email;
        Role = role;
    }
}