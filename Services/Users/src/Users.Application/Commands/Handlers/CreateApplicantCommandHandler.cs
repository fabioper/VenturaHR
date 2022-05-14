using AutoMapper;
using Common;
using Common.Events.Models;
using MassTransit;
using MediatR;
using Users.Infra.Data.Models.Entities;

namespace Users.Application.Commands.Handlers;

public class CreateApplicantCommandHandler : IRequestHandler<CreateCompanyCommand>
{
    private readonly IRepository<Applicant> _repository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public CreateApplicantCommandHandler(
        IRepository<Applicant> repository,
        IMapper mapper,
        IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Unit> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var newApplicant = _mapper.Map<Applicant>(request);
        await _repository.Add(newApplicant);

        var userCreatedEvent = new UserCreated(
            newApplicant.Name,
            newApplicant.Email,
            newApplicant.ExternalId.Value,
            new() { nameof(Applicant) }
        );

        await _publishEndpoint.Publish(userCreatedEvent, cancellationToken);

        return Unit.Value;
    }
}