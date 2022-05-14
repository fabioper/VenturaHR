using FluentValidation;
using Users.Api.DTOs;

namespace Users.Api.Validations;

public class CreateCompanyProfileValidator : AbstractValidator<CreateCompanyProfileRequest>
{
    public CreateCompanyProfileValidator()
    {
        RuleFor(x => x.Identifier).NotEmpty();
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Registration).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
}