using Common.Abstractions;
using Common.Events;
using MassTransit;
using MediatR;
using Users.Api.Models.Entities;

namespace Users.Api.Commands.CreateCompany;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand>
{
    private readonly IRepository<Company> _companyRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateCompanyCommandHandler(
        IRepository<Company> companyRepository,
        IPublishEndpoint publishEndpoint)
    {
        _companyRepository = companyRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Unit> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var newCompany = new Company(
            request.Identifier,
            request.Name,
            request.Email,
            new(request.PhoneNumber),
            new(request.Registration)
        );

        await _companyRepository.Add(newCompany);

        var userCreatedEvent = new CompanyCreatedEvent(newCompany.Name, newCompany.Email, newCompany.Id.ToString());

        await _publishEndpoint.Publish(userCreatedEvent, cancellationToken);
        return Unit.Value;
    }
}