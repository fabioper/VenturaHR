using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Users.Application.DTOs.Responses;
using Users.Application.Services.Contracts;
using Users.CrossCutting.Options;
using Users.CrossCutting.Services;
using Users.Domain.Models.Entities;
using Users.Domain.Models.Enums;

namespace Users.Application.Services.Concretes;

public class TokenService : ITokenService
{
    private readonly ICacheService _redis;
    private readonly TokenSettings _tokenSettings;

    public TokenService(ICacheService redis, TokenSettings tokenSettings)
    {
        _redis = redis;
        _tokenSettings = tokenSettings;
    }

    public async Task<TokenResponse> GenerateToken(User user)
    {
        var accessToken = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken();
        await SaveRefreshToken(user, refreshToken);

        return new TokenResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
        };
    }

    private string GenerateAccessToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserType), user.UserType) ?? string.Empty),
            }),
            Issuer = _tokenSettings.Issuer,
            Audience = _tokenSettings.Audience,
            IssuedAt = DateTime.Now,
            NotBefore = DateTime.Now,
            Expires = DateTime.UtcNow.AddMinutes(_tokenSettings.ExpirationInMinutes),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var accessToken = tokenHandler.WriteToken(token);
        return accessToken;
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private async Task SaveRefreshToken(User user, string refreshToken)
        => await _redis.Set(refreshToken, user.Id.ToString());

    public async Task<string?> GetUserIdFromRefreshToken(string token)
        => await _redis.Get(token);
}
