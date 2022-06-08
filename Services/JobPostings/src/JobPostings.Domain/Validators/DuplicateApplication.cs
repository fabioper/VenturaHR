using JobPostings.Domain.Aggregates.JobApplications;

namespace JobPostings.Domain.Validators;

public interface IDuplicateApplicationValidator : IValidator<JobApplication>
{
}