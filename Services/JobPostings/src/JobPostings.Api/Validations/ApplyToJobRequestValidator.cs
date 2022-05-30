using FluentValidation;
using JobPostings.Application.DTOs.Requests;

namespace JobPostings.Api.Validations;

public class ApplyToJobRequestValidator : AbstractValidator<ApplyToJobRequest>
{
    public ApplyToJobRequestValidator()
    {
        RuleFor(x => x.ApplicantId).Empty();
        RuleFor(x => x.JobPostingId).NotEmpty();
    }
}