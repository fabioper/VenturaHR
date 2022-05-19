using FluentValidation;
using JobPostings.Api.DTOs.Requests;

namespace JobPostings.Api.Validations;

public class PostJobValidation : AbstractValidator<PostJobRequest>
{
    public PostJobValidation()
    {
        RuleFor(x => x.Role).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Location).NotEmpty();
        RuleFor(x => x.Salary).NotEmpty().GreaterThan(0);
        RuleFor(x => x.ExpirationDate).NotEmpty();
    }
}