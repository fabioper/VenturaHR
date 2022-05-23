using Users.Api.DTOs.Responses;
using Users.Api.Models.Entities;

namespace Users.Api.Services.Contracts;

public interface ITokenService
{
    Task<TokenResponse> GenerateToken(User user);
    Task<string?> GetUserIdFromRefreshToken(string token);
}