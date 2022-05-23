using JobPostings.Application.Services.Concretes;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.CompanyAggregate;
using JobPostings.Domain.JobPostingAggregate;
using JobPostings.Infra.Data;
using JobPostings.Infra.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Api.Extensions.DI;

public static class DIExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IJobPostingsService, JobPostingsService>();
    }
    
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IJobPostingRepository, JobPostingRepository>();
    }

    public static void AddDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        var dbConnection = configuration.GetConnectionString("Database");
        services.AddDbContext<ModelContext>(cfg => cfg.UseNpgsql(dbConnection));
    }

    public static void AddCorsPolicy(this IServiceCollection services, out string policyName)
    {
        const string corsConfig = "_corsConfig";

        services.AddCors(config =>
        {
            config.AddPolicy(corsConfig, policyBuilder =>
            {
                policyBuilder.AllowAnyOrigin()
                             .AllowAnyHeader()
                             .AllowAnyMethod();
            });
        });

        policyName = corsConfig;
    }
}