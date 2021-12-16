using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;


namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoBuilderExtensions
    {
        public static DataObjectInfoBuilder WithConcurrencyCheckBehavior(this DataObjectInfoBuilder instance, ConcurrencyCheckBehavior concurrencyCheckBehavior)
            => instance.ReplaceMetadata(Database.ConcurrencyCheckBehavior, concurrencyCheckBehavior);
    }
}
