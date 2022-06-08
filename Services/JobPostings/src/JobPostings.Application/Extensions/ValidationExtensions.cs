using Common.Abstractions;
using JobPostings.Application.Exceptions;
using JobPostings.Domain.Aggregates.JobApplications;
using JobPostings.Domain.Validators;

namespace JobPostings.Application.Extensions;

public static class ValidationExtensions
{
    public static void ValidateAgainst(this JobApplication entity, IValidator<JobApplication> validator)
    {
        var isValid = validator.IsValid(entity, out var brokenRules);

        if (!isValid)
            throw new ValidationFailedException(brokenRules);
    }
}