using AutoMapper;
using Users.Api.Commands;
using Users.Api.Data.Models;

namespace Users.Api.MappingProfiles;

public class UserMapping : Profile
{
    public UserMapping()
    {
        CreateMap<FinishUserProfileCommand, UserProfile>();
    }
}