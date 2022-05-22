using FluentValidation;
using Users.Api.DTOs;
using Users.Api.DTOs.Requests;

namespace Users.Api.Validations;

public class CreateCompanyProfileValidator : AbstractValidator<CreateUserRequest>
{
    public CreateCompanyProfileValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Registration).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
        RuleFor(x => x.ExternalId).NotEmpty();
    }
}