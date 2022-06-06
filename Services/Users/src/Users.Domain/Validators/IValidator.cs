namespace Users.Domain.Validators;

public interface IValidator<in T>
{
    public bool IsValid(T entity, out IDictionary<string, string> brokenRules);
    public IDictionary<string, string> BrokenRules(T entity);
}