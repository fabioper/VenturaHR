using AutoMapper;
using Common;
using MediatR;
using Users.Application.Notifications;
using Users.Infra.Data.Models;

namespace Users.Application.Commands.Handlers;

public class FinishUserProfileCommandHandler : IRequestHandler<FinishUserProfileCommand>
{
    private readonly IMediator _mediator;
    private readonly IRepository<UserProfile> _repository;
    private readonly IMapper _mapper;

    public FinishUserProfileCommandHandler(IMediator mediator, IRepository<UserProfile> repository, IMapper mapper)
    {
        _mediator = mediator;
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(FinishUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<UserProfile>(request);

        await _repository.Add(user);

        var notification = new UserCreatedNotification
        {
            Id = user.Id,
            Name = user.Name,
            ExternalId = user.ExternalId,
            Role = new() { user.Role },
        };

        await _mediator.Publish(notification, cancellationToken);

        return Unit.Value;
    }
}