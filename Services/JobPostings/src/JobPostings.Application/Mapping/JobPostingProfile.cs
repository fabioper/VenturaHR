using AutoMapper;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Application.Mapping;

public class JobPostingProfile : Profile
{
    public JobPostingProfile()
    {
        CreateMap<JobPosting, JobPostingResponse>()
            .ForMember(dto => dto.Salary, opts => opts.MapFrom(m => m.Salary.Value));
    }
}