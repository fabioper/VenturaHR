using JobPostings.CrossCutting.Abstractions;
using JobPostings.CrossCutting.Exceptions;
using JobPostings.Domain.Validators;

namespace JobPostings.Application.Extensions;

public static class ValidationExtensions
{
    public static void ValidateAgainst<T>(this T entity, IValidator<T> validator)
    {
        var isValid = validator.IsValid(entity, out var brokenRules);

        if (!isValid)
            throw new ValidationException(brokenRules);
    }
}