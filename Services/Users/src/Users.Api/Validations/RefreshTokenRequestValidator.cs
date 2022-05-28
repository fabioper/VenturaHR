using FluentValidation;
using Users.Application.DTOs.Requests;

namespace Users.Api.Validations;

public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
        => RuleFor(x => x.RefreshToken).NotEmpty();
}