namespace Common.Abstractions;

public abstract class Entity
{
    public Guid Id { get; protected set; }

    public static bool operator ==(Entity? a, Entity? b)
    {
        if (a is null && b is null)
            return true;

        if (a is null || b is null)
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b) => !(a == b);

    protected bool Equals(Entity other) => Id == other.Id;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        if (obj.GetType() != GetType())
            return false;
        return Equals((Entity)obj);
    }

    public override int GetHashCode() => Id.GetHashCode();
}