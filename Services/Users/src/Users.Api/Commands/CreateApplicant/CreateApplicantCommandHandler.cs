using Common.Abstractions;
using Common.Events;
using MassTransit;
using MediatR;
using Users.Api.Models.Entities;

namespace Users.Api.Commands.CreateApplicant;

public class CreateApplicantCommandHandler : IRequestHandler<CreateApplicantCommand>
{
    private readonly IRepository<Applicant> _repository;
    private readonly IPublishEndpoint _publishEndpoint;

    public CreateApplicantCommandHandler(
        IRepository<Applicant> repository,
        IPublishEndpoint publishEndpoint)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<Unit> Handle(CreateApplicantCommand request, CancellationToken cancellationToken)
    {
        var newApplicant = new Applicant(request.Name, request.Email, request.Identifier);

        await _repository.Add(newApplicant);

        var userCreatedEvent = new ApplicantCreatedEvent(newApplicant.Name,
            newApplicant.Email,
            newApplicant.ExternalId);

        await _publishEndpoint.Publish(userCreatedEvent, cancellationToken);

        return Unit.Value;
    }
}