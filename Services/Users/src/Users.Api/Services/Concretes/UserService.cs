using AutoMapper;
using Common.Events;
using Common.Exceptions;
using MassTransit;
using Users.Api.Data.Repositories;
using Users.Api.DTOs.Requests;
using Users.Api.DTOs.Responses;
using Users.Api.Models.Entities;
using Users.Api.Models.ValueObjects;
using Users.Api.Services.Contracts;

namespace Users.Api.Services.Concretes;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository repository,
        IPublishEndpoint publishEndpoint,
        IMapper mapper)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
    }

    public async Task CreateUser(CreateUserRequest request)
    {
        var newCompany = new User(
            new UserId(request.ExternalId),
            request.Name,
            request.Email,
            new PhoneNumber(request.PhoneNumber),
            new Registration(request.Registration)
        );

        await _repository.Add(newCompany);

        var userCreatedEvent = new CompanyCreatedEvent(
            newCompany.Name, newCompany.Email, newCompany.Id.Value);

        await _publishEndpoint.Publish(userCreatedEvent);
    }

    public async Task<UserProfileResponse> FindUserOfId(string userId)
    {
        var user = await _repository.FindById(new UserId(userId));

        if (user is null)
            throw new EntityNotFoundException(nameof(User));
        
        return _mapper.Map<UserProfileResponse>(user);
    }
}