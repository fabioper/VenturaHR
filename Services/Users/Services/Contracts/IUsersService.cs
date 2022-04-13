using Users.DTOs;

namespace Users.Services.Contracts;

public interface IUsersService
{
    Task RegisterCompany(RegisterCompanyInput dto);
    Task RegisterApplicant(RegisterApplicantInput dto);
}