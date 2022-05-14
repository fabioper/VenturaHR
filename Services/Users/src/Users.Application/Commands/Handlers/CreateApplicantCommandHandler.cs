using Common.Abstractions;
using Common.Events;
using MassTransit;
using MediatR;
using Users.Infra.Data.Models.Entities;

namespace Users.Application.Commands.Handlers;

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
        var newApplicant = new Applicant(request.Identifier, request.Name, request.Email);

        await _repository.Add(newApplicant);

        var userCreatedEvent = new ApplicantCreatedEvent(
            newApplicant.Name,
            newApplicant.Email,
            newApplicant.Id.ToString()
        );

        await _publishEndpoint.Publish(userCreatedEvent, cancellationToken);

        return Unit.Value;
    }
}