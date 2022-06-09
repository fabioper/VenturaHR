using System.Text;
using JobPostings.Api.Common.Constants;
using JobPostings.CrossCutting.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace JobPostings.Api.Common.Extensions.DependencyInjection;

public static class AuthenticationExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtConfig = configuration.GetSection(nameof(TokenSettings)).Get<TokenSettings>();

        var key = Encoding.ASCII.GetBytes(jwtConfig.Secret);
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
                    ValidIssuer = jwtConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtConfig.Audience,
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