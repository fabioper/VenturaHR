using FluentValidation;
using JobPostings.Application.DTOs.Requests;

namespace JobPostings.Api.Validations;

public class ApplicationRequestValidator : AbstractValidator<ApplicationRequest>
{
    public ApplicationRequestValidator()
    {
        RuleFor(x => x.ApplicantId).Empty();
        RuleFor(x => x.JobPostingId).NotEmpty();
    }
}