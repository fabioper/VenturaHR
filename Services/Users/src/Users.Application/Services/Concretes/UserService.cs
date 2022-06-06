using AutoMapper;
using Common.Events;
using Common.Exceptions;
using MassTransit;
using Users.Application.Contracts.Infrastructure;
using Users.Application.DTOs.Requests;
using Users.Application.DTOs.Responses;
using Users.Application.Exceptions;
using Users.Application.Extensions;
using Users.Application.Extensions.Validations;
using Users.Application.Services.Contracts;
using Users.Domain.Models.Entities;
using Users.Domain.Models.Enums;
using Users.Domain.Models.ValueObjects;
using Users.Domain.Repositories;
using Users.Domain.Validators;

namespace Users.Application.Services.Concretes;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ITokenService _tokenService;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IUniquenessValidator _uniquenessValidator;
    private readonly ICacheService _cache;
    private readonly IMapper _mapper;

    public UserService(
        IUserRepository repository,
        ITokenService tokenService,
        IPublishEndpoint publishEndpoint,
        IUniquenessValidator uniquenessValidator,
        IMapper mapper,
        ICacheService cache)
    {
        _repository = repository;
        _tokenService = tokenService;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
        _cache = cache;
        _uniquenessValidator = uniquenessValidator;
    }

    public async Task CreateUser(CreateUserRequest request)
    {
        var newUser = new User(
            request.Name,
            request.Email,
            request.Password.ToHash(),
            new PhoneNumber(request.PhoneNumber),
            new Registration(request.Registration),
            request.UserType
        );

        newUser.Validate(_uniquenessValidator);

        await _repository.Add(newUser);
        await PublishUserCreatedEvent(request, newUser);
    }

    private async Task PublishUserCreatedEvent(CreateUserRequest request, User newCompany)
    {
        object userCreatedEvent = request.UserType switch
        {
            UserType.Company => new CompanyCreatedEvent(
                newCompany.Name, newCompany.Email, newCompany.Id.ToString()),
            UserType.Applicant => new ApplicantCreatedEvent(
                newCompany.Name, newCompany.Email, newCompany.Id.ToString()),
            _ => throw new UnrecognizedUserType(request.UserType.ToString()),
        };

        await _publishEndpoint.Publish(userCreatedEvent);
    }

    public async Task<TokenResponse> Authenticate(LoginRequest request)
    {
        var user = await _repository.FindByEmail(request.Email);

        if (user is null || !request.Password.IsEqualToHash(user.Password))
            throw new InvalidCredentialException();

        return await _tokenService.GenerateToken(user);
    }

    public async Task<TokenResponse> RefreshToken(RefreshTokenRequest request)
    {
        var userId = await _tokenService.GetUserIdFromRefreshToken(request.RefreshToken);
        if (userId is null)
            throw new InvalidRefreshToken();

        var user = await _repository.FindById(Guid.Parse(userId));
        if (user is null)
            throw new EntityNotFoundException(nameof(user));

        return await _tokenService.GenerateToken(user);
    }

    public async Task<UserProfileResponse> GetUserProfile(Guid userId)
    {
        var cachedUser = await _cache.GetAs<UserProfileResponse>(userId.ToString());
        if (cachedUser != null)
            return cachedUser;

        var user = await FindUserOfId(userId);

        var profile = _mapper.Map<UserProfileResponse>(user);
        await _cache.Set(userId.ToString(), profile);

        return profile;
    }

    private async Task<User?> FindUserOfId(Guid userId)
    {
        var user = await _repository.FindById(userId);
        return user ?? throw new EntityNotFoundException(nameof(user));
    }
}