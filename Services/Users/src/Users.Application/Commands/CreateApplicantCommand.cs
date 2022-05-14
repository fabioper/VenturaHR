using MediatR;

namespace Users.Application.Commands;

public class CreateApplicantCommand : IRequest
{
    public string Identifier { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}