using JobPostings.Application.Services.Concretes;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.Repositories;
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
        services.AddScoped<IApplicantRepository, ApplicantRepository>();
        services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
    }

    public static void AddDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ModelContext>(cfg =>
        {
            var dbConnection = configuration.GetConnectionString("Database");
            cfg.UseNpgsql(dbConnection);
        });
    }
}