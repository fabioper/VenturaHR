using AutoMapper;
using Users.Api.DTOs.Responses;
using Users.Api.Models.Entities;

namespace Users.Api.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserProfileResponse>();
    }
}