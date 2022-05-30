using FluentValidation;
using JobPostings.Application.DTOs.Requests;

namespace JobPostings.Api.Validations;

public class CriteriaFullfillmentsRequestValidator : AbstractValidator<CriteriaFullfillmentRequest>
{
    public CriteriaFullfillmentsRequestValidator()
    {
        RuleFor(x => x.CriteriaId).NotEmpty();
        RuleFor(x => x.Value).IsInEnum();
    }
}