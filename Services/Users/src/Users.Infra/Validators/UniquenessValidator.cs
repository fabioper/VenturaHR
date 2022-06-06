using Users.Domain.Models.Entities;
using Users.Domain.Repositories;
using Users.Domain.Validators;

namespace Users.Infra.Validators;

public class UniquenessValidator : IUniquenessValidator
{
    private readonly IUserRepository _userRepository;

    public UniquenessValidator(IUserRepository userRepository) => _userRepository = userRepository;

    public bool IsValid(User entity, out IDictionary<string, string> brokenRules)
    {
        brokenRules = BrokenRules(entity);
        return !brokenRules.Any();
    }

    public IDictionary<string, string> BrokenRules(User entity)
    {
        var brokenRules = new Dictionary<string, string>();

        ValidateEmail(entity, brokenRules);
        ValidateRegistration(entity, brokenRules);

        return brokenRules;
    }

    private void ValidateRegistration(User entity, Dictionary<string, string> rules)
    {
        var userByRegistration = _userRepository.FindByRegistration(entity.Registration).Result;

        if (userByRegistration != null)
            rules.Add(nameof(entity.Registration), "Registration already taken");
    }

    private void ValidateEmail(User entity, IDictionary<string, string> rules)
    {
        var userByEmail = _userRepository.FindByEmail(entity.Email).Result;

        if (userByEmail != null)
            rules.Add(nameof(entity.Email), "Email already taken");
    }
}