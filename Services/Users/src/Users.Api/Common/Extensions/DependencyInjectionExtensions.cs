using System.Text;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using StackExchange.Redis;
using Users.Application.Services.Concretes;
using Users.Application.Services.Contracts;
using Users.CrossCutting.Options;
using Users.Domain.Repositories;
using Users.Infra.Data;
using Users.Infra.Interfaces;
using Users.Infra.Repositories;
using Users.Infra.Services;

namespace Users.Api.Common.Extensions;

public static class DependencyInjectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICacheService, RedisClient>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserService, UserService>();
    }

    public static void AddRepositories(this IServiceCollection services)
        => services.AddScoped<IUserRepository, UserRepository>();

    public static void ConfigureDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        var dbConnection = configuration.GetConnectionString("Database");
        services.AddDbContext<UsersContext>(c => c.UseNpgsql(dbConnection));
    }

    public static void ConfigureJwt(this IServiceCollection services, TokenSettings tokenSettings)
    {
        var key = Encoding.ASCII.GetBytes(tokenSettings.Secret);
        services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
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

    public static void ConfigureMassTransit(this IServiceCollection services, ConfigurationManager configuration)
    {
        var rabbitMqConnection = configuration.GetConnectionString("RabbitMq");
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, config) =>
            {
                config.ConfigureEndpoints(context);
                config.Host(new Uri(rabbitMqConnection));
            });
        });
    }

    public static void ConfigureRedis(this IServiceCollection services, ConfigurationManager configuration)
    {
        var redisConnection = configuration.GetConnectionString("Redis");
        var multiplexer = ConnectionMultiplexer.Connect(redisConnection);
        services.AddSingleton<IConnectionMultiplexer>(multiplexer);
    }

    public static void ConfigureSwagger(this IServiceCollection services)
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