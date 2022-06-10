using System.Reflection;
using Microsoft.OpenApi.Models;

namespace JobPostings.Api.Extensions.DependencyInjection;

public static class SwaggerExtensions
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.IncludeXmlComments(GetXmlFilePath());
            options.AddSecurityDefinition("Bearer", SecurityScheme);
            options.AddSecurityRequirement(SecurityRequirement);
        });
    }

    private static string GetXmlFilePath()
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var filePath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
        return filePath;
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
        Description = "JWT Authorization Header - Exemplo: Bearer [TOKEN]",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    };
}