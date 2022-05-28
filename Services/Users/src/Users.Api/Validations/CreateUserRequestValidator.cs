using FluentValidation;
using Users.Application.DTOs.Requests;

namespace Users.Api.Validations;

public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    private const int CpfLength = 11;
    private const int CnpjLength = 14;
    private const int PhoneNumberLength = 11;
    
    public CreateUserValidator()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();

        RuleFor(x => x.Name).NotEmpty();

        RuleFor(x => x.Registration)
            .NotEmpty()
            .MinimumLength(CpfLength)
            .MaximumLength(CnpjLength);

        RuleFor(x => x.PhoneNumber).NotEmpty().Length(PhoneNumberLength);
    }
}