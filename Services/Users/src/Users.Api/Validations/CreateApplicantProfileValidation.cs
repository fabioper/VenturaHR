using FluentValidation;
using Users.Api.DTOs;

namespace Users.Api.Validations;

public class CreateApplicantProfileValidation : AbstractValidator<CreateApplicantProfileRequest>
{
    public CreateApplicantProfileValidation()
    {
        RuleFor(x => x.Identifier).NotEmpty();
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
    }
}