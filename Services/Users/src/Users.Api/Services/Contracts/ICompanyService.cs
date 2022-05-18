using Users.Api.DTOs.Requests;
using Users.Api.DTOs.Responses;

namespace Users.Api.Services.Contracts;

public interface ICompanyService
{
    Task CreateCompanyProfile(CreateCompanyProfileRequest request);
    Task<CompanyProfileResponse> FindCompanyByExternalId(string externalId);
}