using Users.Domain.Models.Entities;

namespace Users.Domain.Validators;

public interface IUniquenessValidator : IValidator<User>
{
}