namespace Common.Guards;

public static class GuardExtensions
{
    public static void Null<T>(this IGuardClause clause, T input, string parameterName)
    {
        if (input is null)
            throw new ArgumentNullException(parameterName);

        if (string.IsNullOrEmpty(input.ToString()))
            throw new ArgumentException(parameterName);
    }

    public static void NullOrEmpty(this IGuardClause clause, string input, string parameterName)
    {
        Guard.Against.Null(input, parameterName);

        if (string.IsNullOrEmpty(input))
            throw new ArgumentException(parameterName);
    }
}
