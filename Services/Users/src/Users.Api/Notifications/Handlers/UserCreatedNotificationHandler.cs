using Common.Events.Models;
using MassTransit;
using MediatR;

namespace Users.Api.Notifications.Handlers;

public class UserCreatedNotificationHandler : INotificationHandler<UserCreatedNotification>
{
    private readonly IBus _bus;

    public UserCreatedNotificationHandler(IBus bus) => _bus = bus;

    public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
    {
        var userCreated = new UserCreated(
            notification.Name,
            notification.Email,
            notification.ExternalId,
            notification.Role
        );

        await _bus.Publish(userCreated, cancellationToken);
    }
}