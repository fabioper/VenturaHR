using Common.EventBus.Models;
using MassTransit;
using Users.Api.Services.Contracts;

namespace Users.Api.EventBus.Consumers;

public class RegistrationFailedConsumer : IConsumer<RegistrationFailed>
{
    private readonly IKeycloakClient _keycloakClient;
    private readonly ILogger<RegistrationFailedConsumer> _logger;

    public RegistrationFailedConsumer(
        IKeycloakClient keycloakClient,
        ILogger<RegistrationFailedConsumer> logger)
    {
        _keycloakClient = keycloakClient;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<RegistrationFailed> context)
    {
        _logger.LogInformation("Deleting user with ID <{}> from Keycloak", context.Message.AccountId);
        await _keycloakClient.DeleteUser(context.Message.AccountId);
    }
}