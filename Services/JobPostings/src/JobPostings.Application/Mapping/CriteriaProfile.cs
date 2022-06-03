using AutoMapper;
using JobPostings.Application.DTOs.Requests;
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

        CreateMap<CriteriaRequest, Criteria>()
            .ForMember(m => m.Id, opts => opts.MapFrom(_ => Guid.NewGuid()));

        CreateMap<CriteriaAnswerRequest, CriteriaAnswer>()
            .ForMember(m => m.Id, opts => opts.MapFrom(_ => Guid.NewGuid()))
            .ForMember(m => m.CriteriaId, opts => opts.MapFrom(dto => dto.CriteriaId));

        CreateMap<CriteriaAnswer, CriteriaAnswerResponse>();
    }
}