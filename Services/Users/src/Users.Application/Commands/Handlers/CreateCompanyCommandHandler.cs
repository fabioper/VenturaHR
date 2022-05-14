using AutoMapper;
using Common.Abstractions;
using Common.Events;
using MassTransit;
using MediatR;
using Users.Infra.Data.Models.Entities;

namespace Users.Application.Commands.Handlers;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand>
{
    private readonly IMapper _mapper;
    private readonly IRepository<Company> _companyRepository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateCompanyCommandHandler(
        IMapper mapper,
        IRepository<Company> companyRepository,
        IPublishEndpoint publishEndpoint)
    {
        _mapper = mapper;
        _companyRepository = companyRepository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Unit> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var newCompany = _mapper.Map<Company>(request);
        await _companyRepository.Add(newCompany);

        var userCreatedEvent = new CompanyCreatedEvent(newCompany.Name, newCompany.Email, newCompany.Id.ToString());

        await _publishEndpoint.Publish(userCreatedEvent, cancellationToken);
        return Unit.Value;
    }
}