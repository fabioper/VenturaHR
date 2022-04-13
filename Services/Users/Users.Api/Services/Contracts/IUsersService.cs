using Users.Api.DTOs;

namespace Users.Api.Services.Contracts;

public interface IUsersService
{
    Task RegisterCompany(RegisterCompanyInput dto);
    Task RegisterApplicant(RegisterApplicantInput dto);
}