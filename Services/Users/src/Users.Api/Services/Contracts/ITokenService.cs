using Users.Api.Models.Entities;

namespace Users.Api.Services.Contracts;

public interface ITokenService
{
    string GenerateToken(User user);
    string GenerateRefreshToken();
    Task SaveRefreshToken(User user, string refreshToken);
    Task<string?> GetUserIdFromRefreshToken(string token);
}