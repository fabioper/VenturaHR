using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Users.Api.Common.Options;
using Users.Api.Models.Entities;
using Users.Api.Models.Enums;
using Users.Api.Services.Contracts;
using static Microsoft.IdentityModel.Tokens.SecurityAlgorithms;

namespace Users.Api.Services.Concretes;

public class TokenService : ITokenService
{
    private readonly IRedisClient _redis;
    private readonly TokenSettings _tokenSettings;

    public TokenService(IRedisClient redis, TokenSettings tokenSettings)
    {
        _redis = redis;
        _tokenSettings = tokenSettings;
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserType), user.UserType) ?? string.Empty),
            }),
            Issuer = _tokenSettings.Issuer,
            Audience = _tokenSettings.Audience,
            Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.ExpirationInMinutes),
            SigningCredentials = new(new SymmetricSecurityKey(key), HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public async Task SaveRefreshToken(User user, string refreshToken)
        => await _redis.Set(refreshToken, user.Id.Value);

    public async Task<string?> GetUserIdFromRefreshToken(string token)
        => await _redis.Get(token);
}
