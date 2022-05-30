using FluentValidation;
using JobPostings.Application.DTOs.Requests;

namespace JobPostings.Api.Validations;

public class PostJobCriteriaRequestValidator : AbstractValidator<PostJobCriteriaRequest>
{
    public PostJobCriteriaRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Weight).IsInEnum();
        RuleFor(x => x.MininumDesiredProfile).IsInEnum();
    }
}