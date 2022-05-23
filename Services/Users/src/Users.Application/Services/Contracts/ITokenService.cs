using Users.Application.DTOs.Responses;
using Users.Domain.Models.Entities;

namespace Users.Application.Services.Contracts;

public interface ITokenService
{
    Task<TokenResponse> GenerateToken(User user);
    Task<string?> GetUserIdFromRefreshToken(string token);
}