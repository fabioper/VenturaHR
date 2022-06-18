using AutoMapper;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Domain.Aggregates.Applicants;

namespace JobPostings.Application.Mapping;

public class ApplicantProfile : Profile
{
    public ApplicantProfile()
    {
        CreateMap<Applicant, ApplicantResponse>();
    }
}