using AutoMapper;
using Users.Application.Commands;
using Users.Infra.Data.Models;

namespace Users.Api.MappingProfiles;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<FinishUserProfileCommand, UserProfile>();
    }
}