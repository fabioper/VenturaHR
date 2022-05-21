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

public class CompanyService : ICompanyService
{
    private readonly IUserRepository<Company> _repository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public CompanyService(
        IUserRepository<Company> repository,
        IPublishEndpoint publishEndpoint,
        IMapper mapper)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
    }

    public async Task CreateCompanyProfile(CreateCompanyProfileRequest request)
    {
        var newCompany = new Company(
            request.Name,
            request.Email,
            new PhoneNumber(request.PhoneNumber),
            new Registration(request.Registration),
            new UserId(request.ExternalId)
        );

        await _repository.Add(newCompany);

        var userCreatedEvent = new CompanyCreatedEvent(
            newCompany.Name, newCompany.Email, newCompany.Id.Value);

        await _publishEndpoint.Publish(userCreatedEvent);
    }

    public async Task<CompanyProfileResponse> FindCompanyByExternalId(string externalId)
    {
        var company = await _repository.FindById(new UserId(externalId));

        if (company is null)
            throw new EntityNotFoundException(nameof(Company));
        
        return _mapper.Map<CompanyProfileResponse>(company);
    }
}