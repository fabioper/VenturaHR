using FluentValidation;
using JobPostings.Application.DTOs.Requests;

namespace JobPostings.Application.Validations;

public class CreateJobPostingRequestValidation : AbstractValidator<CreateJobPostingRequest>
{
    public CreateJobPostingRequestValidation()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Location).NotEmpty();
        RuleFor(x => x.Salary).NotEmpty().GreaterThan(0);
        RuleFor(x => x.ExpirationDate).NotEmpty();
        RuleFor(x => x.CompanyId).Empty();
    }
}