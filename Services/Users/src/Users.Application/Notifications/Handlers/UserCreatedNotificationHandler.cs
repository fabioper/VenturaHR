using Common.Events.Models;
using MassTransit;
using MediatR;

namespace Users.Application.Notifications.Handlers;

public class UserCreatedNotificationHandler : INotificationHandler<UserCreatedNotification>
{
    private readonly IPublishEndpoint _publishEndpoint;

    public UserCreatedNotificationHandler(IPublishEndpoint publishEndpoint) =>
        _publishEndpoint = publishEndpoint;

    public async Task Handle(UserCreatedNotification notification, CancellationToken cancellationToken)
    {
        var userCreated = new UserCreated(notification.Name,
                                          notification.Email,
                                          notification.ExternalId,
                                          notification.Role);

        await _publishEndpoint.Publish(userCreated, cancellationToken);
    }
}