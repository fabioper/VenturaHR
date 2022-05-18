using AutoMapper;
using Common.Events;
using Common.Exceptions;
using MassTransit;
using Users.Api.Data.Repositories;
using Users.Api.DTOs.Requests;
using Users.Api.DTOs.Responses;
using Users.Api.Models.Entities;
using Users.Api.Services.Contracts;

namespace Users.Api.Services.Concretes;

public class ApplicantService : IApplicantService
{
    private readonly IUserRepository<Applicant> _repository;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public ApplicantService(
        IUserRepository<Applicant> repository,
        IPublishEndpoint publishEndpoint,
        IMapper mapper)
    {
        _repository = repository;
        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
    }

    public async Task CreateApplicantProfile(CreateApplicantProfileRequest request)
    {
        var newApplicant = new Applicant(request.Name, request.Email, request.ExternalId);

        await _repository.Add(newApplicant);

        var userCreatedEvent = new ApplicantCreatedEvent(newApplicant.Name,
            newApplicant.Email,
            newApplicant.ExternalId);

        await _publishEndpoint.Publish(userCreatedEvent);
    }

    public async Task<ApplicantProfileResponse> FindApplicantByExternalId(string externalId)
    {
        var applicant = await _repository.FindByExternalId(externalId);

        if (applicant is null)
            throw new EntityNotFoundException(nameof(Applicant));

        return _mapper.Map<ApplicantProfileResponse>(applicant);
    }
}