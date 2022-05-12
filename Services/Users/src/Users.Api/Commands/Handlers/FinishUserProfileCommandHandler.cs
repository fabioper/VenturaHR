using AutoMapper;
using Common;
using MediatR;
using Users.Api.Commands;
using Users.Api.Data.Models;
using Users.Api.Notifications;

namespace Users.Api.Handlers;

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

        await _mediator.Publish(new UserCreatedNotification(), cancellationToken);

        return Unit.Value;
    }
}