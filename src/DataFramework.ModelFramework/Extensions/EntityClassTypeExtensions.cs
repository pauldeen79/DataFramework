namespace DataFramework.ModelFramework.Extensions
{
    internal static class EntityClassTypeExtensions
    {
        internal static bool HasPropertySetter(this EntityClassType instance)
            => instance == EntityClassType.Poco || instance == EntityClassType.ObservablePoco;

        internal static bool IsImmutable(this EntityClassType instance)
            => instance == EntityClassType.ImmutableClass || instance == EntityClassType.Record;
    }
}
