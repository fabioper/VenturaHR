using FluentValidation;
using JobPostings.Application.DTOs.Requests;

namespace JobPostings.Application.Validations;

public class JobApplicationRequestValidator : AbstractValidator<JobApplicationRequest>
{
    public JobApplicationRequestValidator()
    {
        RuleFor(x => x.ApplicantId).NotEmpty();
        RuleFor(x => x.JobPostingId).NotEmpty();
    }
}