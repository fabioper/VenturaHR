using JobPostings.Application.Services.Concretes;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.Repositories;
using JobPostings.Domain.Validators;
using JobPostings.Infra.Email;
using JobPostings.Infra.Persistence;
using JobPostings.Infra.Persistence.Repositories;
using JobPostings.Infra.Validators;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Api.Common.Extensions.DependencyInjection;

public static class ServicesExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IJobPostingsService, JobPostingsService>();
        services.AddScoped<IJobApplicationService, JobApplicationService>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IJobPostingRepository, JobPostingRepository>();
        services.AddScoped<IApplicantRepository, ApplicantRepository>();
        services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
        services.AddScoped<IEmailService, EmailService>();
    }

    public static void AddDbContext(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDbContext<ModelContext>(config =>
        {
            var connection = configuration.GetConnectionString("Database");
            config.UseNpgsql(connection);
        });
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IDuplicateApplicationValidator, DuplicateApplicationValidator>();
    }
}