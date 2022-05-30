using FluentValidation;
using JobPostings.Application.DTOs.Requests;

namespace JobPostings.Api.Validations;

public class CriteriaRequestValidator : AbstractValidator<CriteriaRequest>
{
    public CriteriaRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Weight).IsInEnum();
        RuleFor(x => x.MininumDesiredProfile).IsInEnum();
    }
}