using AutoMapper;
using JobPostings.Application.DTOs.Requests;
using JobPostings.Application.DTOs.Responses;
using JobPostings.Domain.Aggregates.Criterias;
using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Common;

namespace JobPostings.Application.Mapping;

public class CriteriaProfile : Profile
{
    public CriteriaProfile()
    {
        CreateMap<Criteria, CriteriaResponse>()
            .ForMember(dto => dto.Id, opts => opts.MapFrom(m => m.Id.Id))
            .ForMember(dto => dto.DesiredProfile, opts => opts.MapFrom(m => m.DesiredProfile));

        CreateMap<CriteriaRequest, Criteria>()
            .ForMember(m => m.Id, opts => opts.MapFrom(_ => new CriteriaId()));

        CreateMap<CriteriaAnswerRequest, CriteriaAnswer>()
            .ForMember(m => m.Id, opts => opts.MapFrom(_ => new CriteriaAnswerId()))
            .ForMember(m => m.CriteriaId, opts => opts.MapFrom(dto => new CriteriaId(dto.CriteriaId)));

        CreateMap<CriteriaAnswer, CriteriaAnswerResponse>();
    }
}