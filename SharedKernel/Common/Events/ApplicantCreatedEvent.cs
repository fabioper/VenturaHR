namespace Common.Events;

public record ApplicantCreatedEvent(
    string Name,
    string Email,
    string Identifier);