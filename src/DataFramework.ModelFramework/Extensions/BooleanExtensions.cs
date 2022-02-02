namespace DataFramework.ModelFramework.Extensions;

internal static class BooleanExtensions
{
    internal static Visibility ToVisibility(this bool isVisible)
        => isVisible
            ? Visibility.Public
            : Visibility.Internal;
}
