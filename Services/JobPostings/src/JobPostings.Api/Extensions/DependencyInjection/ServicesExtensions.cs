using JobPostings.Application.Services.Concretes;
using JobPostings.Application.Services.Contracts;
using JobPostings.CrossCutting.Settings;
using JobPostings.Domain.Repositories;
using JobPostings.Domain.Validators;
using JobPostings.Infra.Email;
using JobPostings.Infra.Persistence;
using JobPostings.Infra.Persistence.Repositories;
using JobPostings.Infra.Validators;
using Microsoft.EntityFrameworkCore;

namespace JobPostings.Api.Extensions.DependencyInjection;

public static class ServicesExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IJobPostingsService, JobPostingsService>();
        services.AddScoped<IJobApplicationService, JobApplicationService>();
        services.AddScoped<IExpiringJobsNotifierService, ExpiringJobsService>();
        services.AddScoped<IEmailService, EmailService>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IJobPostingRepository, JobPostingRepository>();
        services.AddScoped<IApplicantRepository, ApplicantRepository>();
        services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
    }

    public static void AddDbContext(this IServiceCollection services, string connection)
    {
        services.AddDbContext<JobPostingsContext>(config => config.UseNpgsql(connection));
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddScoped<IDuplicateApplicationValidator, DuplicateApplicationValidator>();
    }
}