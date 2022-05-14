using MediatR;

namespace Users.Application.Commands;

public class CreateCompanyCommand : IRequest
{
    public string Identifier { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Registration { get; set; }
}