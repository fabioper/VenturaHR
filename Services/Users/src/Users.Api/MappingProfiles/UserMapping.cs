using AutoMapper;
using Users.Application.Commands;
using Users.Infra.Data.Models;
using Users.Infra.Data.Models.Entities;

namespace Users.Api.MappingProfiles;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<CreateCompanyCommand, Applicant>();
    }
}