using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static class BooleanExtensions
    {
        public static Visibility ToVisibility(this bool isVisible)
            => isVisible
                ? Visibility.Public
                : Visibility.Internal;
    }
}
