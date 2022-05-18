using FluentValidation;
using Users.Api.DTOs;
using Users.Api.DTOs.Requests;

namespace Users.Api.Validations;

public class CreateApplicantProfileValidation : AbstractValidator<CreateApplicantProfileRequest>
{
    public CreateApplicantProfileValidation()
    {
        RuleFor(x => x.Email).EmailAddress().NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.ExternalId).NotEmpty();
    }
}