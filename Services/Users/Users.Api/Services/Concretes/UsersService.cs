using Common.EventBus.Models;
using MassTransit;
using Users.Api.DTOs;
using Users.Api.Services.Concretes.Models;
using Users.Api.Services.Contracts;

namespace Users.Api.Services.Concretes;

public class UsersService : IUsersService
{
    private readonly IKeycloakClient _keycloakClient;
    private readonly IPublishEndpoint _publishEndpoint;

    public UsersService(IKeycloakClient keycloakClient, IPublishEndpoint publishEndpoint)
    {
        _keycloakClient = keycloakClient;
        _publishEndpoint = publishEndpoint;
    }

    public async Task RegisterCompany(RegisterCompanyInput dto)
    {
        var companyUser = new UserRepresentation(
            dto.Email, dto.Name, new[] { "companies" });
        var addedUser = await _keycloakClient.RegisterUser(companyUser);
        await _publishEndpoint.Publish(new CompanyRegistered(addedUser.Id));
    }

    public async Task RegisterApplicant(RegisterApplicantInput dto)
    {
        var applicantUser = new UserRepresentation(
            dto.Email, dto.Name, new[] { "applicants" });
        var addedUser = await _keycloakClient.RegisterUser(applicantUser);
        await _publishEndpoint.Publish(new ApplicantRegistered(addedUser.Id));
    }
}