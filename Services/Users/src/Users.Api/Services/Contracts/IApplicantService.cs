using Users.Api.DTOs.Requests;
using Users.Api.DTOs.Responses;

namespace Users.Api.Services.Contracts;

public interface IApplicantService
{
    Task CreateApplicantProfile(CreateApplicantProfileRequest request);
    Task<ApplicantProfileResponse> FindApplicantByExternalId(string externalId);
}