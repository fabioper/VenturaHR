namespace Common.Events;

#nullable disable

public record CompanyCreatedEvent
{
    public string Identifier { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
}