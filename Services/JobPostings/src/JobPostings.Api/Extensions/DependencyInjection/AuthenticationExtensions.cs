using System.Text;
using JobPostings.Api.Common.Constants;
using JobPostings.CrossCutting.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace JobPostings.Api.Extensions.DependencyInjection;

public static class AuthenticationExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, TokenSettings tokenSettings)
    {

        var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = tokenSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = tokenSettings.Audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });
    }

    public static void AddPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policy.CompanyOnly, policyBuilder => policyBuilder.RequireRole("Company"));
            options.AddPolicy(Policy.ApplicantOnly, policyBuilder => policyBuilder.RequireRole("Applicant"));
        });
    }
}