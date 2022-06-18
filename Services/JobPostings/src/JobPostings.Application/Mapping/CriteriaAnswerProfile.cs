using AutoMapper;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Domain.Aggregates.JobApplications;

namespace JobPostings.Application.Mapping;

public class CriteriaAnswerProfile : Profile
{
    public CriteriaAnswerProfile()
    {
        CreateMap<CriteriaAnswer, CriteriaAnswerResponse>()
            .ForMember(dto => dto.CriteriaId, opts => opts.MapFrom(m => m.Criteria.Id))
            .ForMember(dto => dto.CriteriaTitle, opts => opts.MapFrom(m => m.Criteria.Title))
            .ForMember(dto => dto.CriteriaDescription, opts => opts.MapFrom(m => m.Criteria.Description));
    }
}