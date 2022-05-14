#nullable disable

using MediatR;

namespace Users.Api.Commands.CreateApplicant;

public record CreateApplicantCommand : IRequest
{
    public string Identifier { get; init; }
    public string Name { get; init; }
    public string Email { get; init; }
}