using AutoMapper;
using Users.Application.DTOs.Responses;
using Users.Domain.Models.Entities;

namespace Users.Application.MappingProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserProfileResponse>()
            .ForMember(dto => dto.Id, opts => opts.MapFrom(m => m.Id.Value))
            .ForMember(dto => dto.PhoneNumber, opts => opts.MapFrom(m => m.PhoneNumber.Value))
            .ForMember(dto => dto.Registration, opts => opts.MapFrom(m => m.Registration.Number));
    }
}