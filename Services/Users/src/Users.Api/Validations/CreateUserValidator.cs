using FluentValidation;
using Users.Application.DTOs.Requests;

namespace Users.Api.Validations;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Registration).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty();
    }
}