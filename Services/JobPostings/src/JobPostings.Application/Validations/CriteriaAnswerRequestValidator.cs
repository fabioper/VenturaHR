using FluentValidation;
using JobPostings.Application.DTOs.Requests;

namespace JobPostings.Application.Validations;

public class CriteriaAnswerRequestValidator : AbstractValidator<CriteriaAnswerRequest>
{
    public CriteriaAnswerRequestValidator()
    {
        RuleFor(x => x.CriteriaId).NotEmpty();
        RuleFor(x => x.Value).NotEmpty().InclusiveBetween(1, 5);
    }
}