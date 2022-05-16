using JobPostings.Api.Config;
using JobPostings.Api.Constants;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace JobPostings.Api.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, ConfigurationManager configuration)
    {
        var jwtConfig = configuration.GetSection(nameof(JwtConfig)).Get<JwtConfig>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = jwtConfig.Authority;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtConfig.Audience,
                    ValidateLifetime = true,
                };
            });
    }

    public static void AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(Policy.CompanyOnly, policyBuilder => policyBuilder.RequireRole("company"));
            options.AddPolicy(Policy.ApplicantOnly, policyBuilder => policyBuilder.RequireRole("applicant"));
        });
    }

    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", SecurityScheme);
            c.AddSecurityRequirement(SecurityRequirement);
        });
    }

    private static OpenApiSecurityRequirement SecurityRequirement => new()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            Array.Empty<string>()
        },
    };

    private static OpenApiSecurityScheme SecurityScheme => new()
    {
        Description =
            "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
            "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
            "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    };
}