using AutoMapper;
using Users.Api.DTOs.Responses;
using Users.Api.Models.Entities;

namespace Users.Api.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Company, CompanyProfileResponse>();
        CreateMap<Applicant, ApplicantProfileResponse>();
    }
}