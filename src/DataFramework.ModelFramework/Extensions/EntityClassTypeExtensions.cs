namespace DataFramework.ModelFramework.Extensions
{
    public static class EntityClassTypeExtensions
    {
        public static bool HasPropertySetter(this EntityClassType instance)
            => instance == EntityClassType.Poco || instance == EntityClassType.ObservablePoco;

        public static bool IsImmutable(this EntityClassType instance)
            => instance == EntityClassType.ImmutableClass || instance == EntityClassType.Record;
    }
}
