namespace Common.Events;

#nullable disable

public record CompanyCreatedEvent(
    string Name,
    string Email,
    string Identifier);