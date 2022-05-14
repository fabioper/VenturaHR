using MediatR;

#nullable disable

namespace Users.Application.Commands;

public record CreateCompanyCommand : IRequest
{
    public string Identifier { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
    public string PhoneNumber { get; init; }
    public string Registration { get; init; }
}