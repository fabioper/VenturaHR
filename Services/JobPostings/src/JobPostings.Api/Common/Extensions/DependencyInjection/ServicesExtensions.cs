using JobPostings.Application.Services.Concretes;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.Repositories;
using JobPostings.Infra.Persistence;
using JobPostings.Infra.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Api.Common.Extensions.DependencyInjection;

public static class ServicesExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IJobPostingsService, JobPostingsService>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IJobPostingRepository, JobPostingRepository>();
        services.AddScoped<IApplicantRepository, ApplicantRepository>();
        services.AddScoped<IJobApplicationRepository, ApplicationRepository>();
    }

    public static void AddDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ModelContext>(config =>
        {
            var connection = configuration.GetConnectionString("Database");
            config.UseNpgsql(connection);
        });
    }
}