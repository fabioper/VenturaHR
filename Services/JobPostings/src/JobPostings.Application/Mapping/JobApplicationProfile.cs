using AutoMapper;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Domain.Aggregates.JobApplications;

namespace JobPostings.Application.Mapping;

public class JobApplicationProfile : Profile
{
    public JobApplicationProfile()
    {
        CreateMap<JobApplication, JobApplicationResponse>()
            .ForMember(dto => dto.Id, opts => opts.MapFrom(m => m.Id.Id));
    }
}