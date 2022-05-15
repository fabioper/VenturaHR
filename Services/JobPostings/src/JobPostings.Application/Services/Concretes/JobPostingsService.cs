using Common.Abstractions;
using JobPostings.Application.Models.Inputs;
using JobPostings.Application.Services.Contracts;
using JobPostings.Domain.JobPostingAggregate;

namespace JobPostings.Application.Services.Concretes;

public class JobPostingsService : IJobPostingsService
{
    private readonly IRepository<JobPosting> _jobPostingsRepository;

    public JobPostingsService(IRepository<JobPosting> jobPostingsRepository) =>
        _jobPostingsRepository = jobPostingsRepository;

    public async Task PostJob(PostJobInput input)
    {
        var newJob = new JobPosting(
            input.Role,
            input.Description,
            input.Location,
            input.Salary,
            input.ExpirationDate,
            input.CompanyId
        );

        await _jobPostingsRepository.Add(newJob);
    }
}