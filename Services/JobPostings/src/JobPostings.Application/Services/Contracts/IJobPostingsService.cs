using JobPostings.Application.Models.Inputs;

namespace JobPostings.Application.Services.Contracts;

public interface IJobPostingsService
{
    Task PostJob(PostJobInput input);
}