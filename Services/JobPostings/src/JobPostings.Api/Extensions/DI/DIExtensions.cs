using JobPostings.Application.Services.Concretes;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.CompanyAggregate;
using JobPostings.Domain.JobPostingAggregate;
using JobPostings.Infra.Repositories;

namespace JobPostings.Api.Extensions;

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
}