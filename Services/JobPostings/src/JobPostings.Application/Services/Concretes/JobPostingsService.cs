using Common.Abstractions;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.JobPostingAggregate;

namespace JobPostings.Application.Services.Concretes;

public class JobPostingsService : IJobPostingsService
{
    private readonly IRepository<JobPosting> _jobPostingsRepository;

    public JobPostingsService(IRepository<JobPosting> jobPostingsRepository) =>
        _jobPostingsRepository = jobPostingsRepository;
}