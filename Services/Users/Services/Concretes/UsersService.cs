using Users.DTOs;
using Users.Services.Concretes.Models;
using Users.Services.Contracts;

namespace Users.Services.Concretes;

public class UsersService : IUsersService
{
    private readonly IKeycloakClient _keycloakClient;

    public UsersService(IKeycloakClient keycloakClient)
    {
        _keycloakClient = keycloakClient;
    }

    public async Task RegisterCompany(RegisterCompanyInput dto)
    {
        var companyUser = new UserRepresentation(
            dto.Email, dto.Name, new[] { "companies" });
        await _keycloakClient.RegisterUser(companyUser);
    }

    public async Task RegisterApplicant(RegisterApplicantInput dto)
    {
        var applicantUser = new UserRepresentation(
            dto.Email, dto.Name, new[] { "applicants" });
        await _keycloakClient.RegisterUser(applicantUser);
    }
}