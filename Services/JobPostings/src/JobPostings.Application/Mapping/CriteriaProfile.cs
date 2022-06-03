using AutoMapper;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Domain.Aggregates.Criterias;
using JobPostings.Domain.Aggregates.JobApplications;

namespace JobPostings.Application.Mapping;

public class CriteriaProfile : Profile
{
    public CriteriaProfile()
    {
        CreateMap<Criteria, CriteriaResponse>()
            .ForMember(dto => dto.DesiredProfile, opts => opts.MapFrom(m => m.DesiredProfile));

        CreateMap<CriteriaAnswer, CriteriaAnswerResponse>();
    }
}