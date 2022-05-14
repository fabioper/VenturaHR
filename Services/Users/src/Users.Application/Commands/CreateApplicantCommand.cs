using MediatR;

#nullable disable

namespace Users.Application.Commands;

public record CreateApplicantCommand : IRequest
{
    public string Identifier { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
}