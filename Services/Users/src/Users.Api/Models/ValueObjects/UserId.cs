using Common.Abstractions;
using Common.Guards;

namespace Users.Api.Models.ValueObjects;

public record UserId : EntityId
{
    public Guid Value { get; private set; }

    public UserId()
    {
        Value = Guid.NewGuid();
    }

    public UserId(Guid value) => Value = value;

    public UserId(string id)
    {
        Guard.Against.NullOrEmpty(id, nameof(id));
        Value = Guid.Parse(id);
    }

    public override string ToString() => Value.ToString();
}