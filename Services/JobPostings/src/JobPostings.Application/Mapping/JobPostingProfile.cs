using AutoMapper;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Domain.Aggregates.JobPostings;

namespace JobPostings.Application.Mapping;

public class JobPostingProfile : Profile
{
    public JobPostingProfile()
    {
        CreateMap<JobPosting, JobPostingResponse>()
            .ForMember(dto => dto.Location, opts => opts.MapFrom(m => m.Location.Place))
            .ForMember(dto => dto.Salary, opts => opts.MapFrom(m => m.Salary.Value))
            .ForMember(dto => dto.Title, opts => opts.MapFrom(m => m.Title.Value))
            .ForMember(dto => dto.ExpirationDate, opts => opts.MapFrom(m => m.ExpireAt.Date));
    }
}