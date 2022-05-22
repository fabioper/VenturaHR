using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Users.Api.DTOs.Responses;
using Users.Api.Models.Entities;
using Users.Api.Models.Enums;
using Users.Api.Services.Contracts;

namespace Users.Api.Services.Concretes;

public class TokenService : ITokenService
{
    public TokenResponse GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(@"?vb*Vvk-tRmgB8za$Z^as*WP@%?SdA45rG*x+WqJC9UC@x&hHZ3k@LKt?$65+Sk^?wnQtu$Et@$TN*g84YVqm");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserType), user.UserType) ?? string.Empty),
            }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return new()
        {
            AccessToken = tokenHandler.WriteToken(token),
        };
    }
}
