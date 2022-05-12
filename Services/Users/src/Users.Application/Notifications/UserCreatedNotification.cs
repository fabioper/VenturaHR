using MediatR;

namespace Users.Application.Notifications;

public class UserCreatedNotification : INotification
{
    public long Id { get; set; }
    public string ExternalId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<string> Role { get; set; }
}