using Users.Api.DTOs.Responses;
using Users.Api.Models.Entities;

namespace Users.Api.Services.Contracts;

public interface ITokenService
{
    TokenResponse GenerateToken(User user);
}