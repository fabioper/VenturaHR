using FluentValidation;
using JobPostings.Application.DTOs.Requests;

namespace JobPostings.Api.Validations;

public class PostJobRequestValidation : AbstractValidator<PostJobRequest>
{
    public PostJobRequestValidation()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Location).NotEmpty();
        RuleFor(x => x.Salary).NotEmpty().GreaterThan(0);
        RuleFor(x => x.ExpirationDate).NotEmpty();
        RuleFor(x => x.CompanyId).Empty();
    }
}