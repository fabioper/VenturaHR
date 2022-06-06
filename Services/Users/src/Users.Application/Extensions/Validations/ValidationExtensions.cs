using Users.Application.Exceptions;
using Users.Domain.Models.Entities;
using Users.Domain.Validators;

namespace Users.Application.Extensions.Validations;

public static class ValidationExtensions
{
    public static void Validate(this User user, IValidator<User> validator)
    {
        var isValid = validator.IsValid(user, out var brokenRules);

        if (!isValid)
            throw new ValidationFailedException(brokenRules);
    }
}