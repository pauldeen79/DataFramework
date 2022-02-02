namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoExtensions
{
    internal static IEnumerable<IDataObjectInfo> WithAdditionalDataObjectInfos(this IDataObjectInfo instance)
    {
        yield return instance;
        foreach (var item in GetCustomMembersFromMetadata<IDataObjectInfo>(instance, Shared.CustomDataObjectInfo))
        {
            yield return item;
        }
    }

    internal static EntityClassType GetEntityClassType(this IDataObjectInfo instance, EntityClassType defaultValue)
        => instance
            .Metadata
            .GetValue(Entities.EntityClassType, () => defaultValue);

    internal static RenderMetadataAsAttributesTypes GetRenderMetadataAsAttributesType(this IDataObjectInfo instance,
                                                                                      RenderMetadataAsAttributesTypes defaultValue)
        => instance
            .Metadata
            .GetValue(Entities.RenderMetadataAsAttributesType, () => defaultValue);

    internal static string GetEntitiesNamespace(this IDataObjectInfo instance)
        => instance.Metadata.GetStringValue(Entities.Namespace)
            .WhenNullOrEmpty(() => instance.TypeName?.GetNamespaceWithDefault(string.Empty) ?? string.Empty);

    internal static string GetEntityBuildersNamespace(this IDataObjectInfo instance)
        => instance.Metadata.GetStringValue(Builders.Namespace)
            .WhenNullOrEmpty(() => instance.GetEntitiesNamespace());

    internal static string GetEntityIdentitiesNamespace(this IDataObjectInfo instance)
        => instance.Metadata.GetStringValue(Identities.Namespace)
            .WhenNullOrEmpty(() => instance.GetEntitiesNamespace());

    internal static string GetQueriesNamespace(this IDataObjectInfo instance)
        => instance.Metadata.GetStringValue(Queries.Namespace)
            .WhenNullOrEmpty(() => instance.GetEntitiesNamespace());

    internal static string GetRepositoriesNamespace(this IDataObjectInfo instance)
        => instance.Metadata.GetStringValue(Repositories.Namespace)
            .WhenNullOrEmpty(() => instance.GetEntitiesNamespace());

    internal static string GetRepositoriesInterfaceNamespace(this IDataObjectInfo instance)
        => instance.Metadata.GetStringValue(Repositories.InterfaceNamespace)
            .WhenNullOrEmpty(() => instance.GetRepositoriesNamespace());

    internal static string GetCommandProvidersNamespace(this IDataObjectInfo instance)
        => instance.Metadata.GetStringValue(CommandProviders.Namespace)
            .WhenNullOrEmpty(() => instance.GetEntitiesNamespace());

    internal static string GetCommandEntityProvidersNamespace(this IDataObjectInfo instance)
        => instance.Metadata.GetStringValue(CommandEntityProviders.Namespace)
            .WhenNullOrEmpty(() => instance.GetEntitiesNamespace());

    internal static string GetQueryFieldProvidersNamespace(this IDataObjectInfo instance)
        => instance.Metadata.GetStringValue(QueryFieldProviders.Namespace)
            .WhenNullOrEmpty(() => instance.GetEntitiesNamespace());

    internal static string GetEntityRetrieverSettingsNamespace(this IDataObjectInfo instance)
        => instance.Metadata.GetStringValue(EntityRetrieverSettings.Namespace)
            .WhenNullOrEmpty(() => instance.GetEntitiesNamespace());

    internal static string GetEntityMapperNamespace(this IDataObjectInfo instance)
        => instance.Metadata.GetStringValue(EntityMappers.Namespace)
            .WhenNullOrEmpty(() => instance.GetEntitiesNamespace());

    internal static string GetEntityFullName(this IDataObjectInfo instance)
    {
        var ns = instance.GetEntitiesNamespace();
        return string.IsNullOrEmpty(ns)
            ? instance.Name
            : $"{ns}.{instance.Name}";
    }

    internal static string GetEntityIdentityFullName(this IDataObjectInfo instance)
    {
        var ns = instance.GetEntityIdentitiesNamespace();
        return string.IsNullOrEmpty(ns)
            ? $"{instance.Name}Identity"
            : $"{ns}.{instance.Name}Identity";
    }

    internal static string GetEntityBuilderFullName(this IDataObjectInfo instance)
    {
        var ns = instance.GetEntityBuildersNamespace();
        return string.IsNullOrEmpty(ns)
            ? $"{instance.Name}Builder"
            : $"{ns}.{instance.Name}Builder";
    }

    internal static string GetEntityRetrieverFullName(this IDataObjectInfo instance)
    {
        var ns = instance.GetEntityRetrieverSettingsNamespace();
        return string.IsNullOrEmpty(ns)
            ? $"{instance.Name}PagedEntityRetrieverSettings"
            : $"{ns}.{instance.Name}PagedEntityRetrieverSettings";
    }

    internal static IEnumerable<IFieldInfo> GetIdentityFields(this IDataObjectInfo instance)
        => instance.Fields.Where(x => (x.IsIdentityField || x.IsSqlIdentity()) && !x.SkipFieldOnFind());

    internal static IEnumerable<IFieldInfo> GetUpdateConcurrencyCheckFields(this IDataObjectInfo instance)
    {
        var concurrencyCheckBehavior = instance.GetConcurrencyCheckBehavior();
        return instance.Fields.Where(fieldInfo => IsUpdateConcurrencyCheckField(instance, fieldInfo, concurrencyCheckBehavior));
    }

    internal static bool IsUpdateConcurrencyCheckField(this IDataObjectInfo instance,
                                                       IFieldInfo fieldInfo,
                                                       ConcurrencyCheckBehavior concurrencyCheckBehavior)
        => concurrencyCheckBehavior != ConcurrencyCheckBehavior.NoFields
            &&
            (
                concurrencyCheckBehavior == ConcurrencyCheckBehavior.AllFields
                || fieldInfo.UseForConcurrencyCheck
                ||
                (
                    !fieldInfo.IsComputed
                    && fieldInfo.IsPersistable
                    && (fieldInfo.IsIdentityField || fieldInfo.IsSqlIdentity())
                )
            );

    internal static ConcurrencyCheckBehavior GetConcurrencyCheckBehavior(this IDataObjectInfo instance)
        => (ConcurrencyCheckBehavior)Enum.Parse(typeof(ConcurrencyCheckBehavior), instance.Metadata.Any(md => md.Name == Database.ConcurrencyCheckBehavior)
            ? instance.Metadata.First(md => md.Name == Database.ConcurrencyCheckBehavior).Value.ToStringWithNullCheck()
            : ConcurrencyCheckBehavior.NoFields.ToString());

    private static IEnumerable<T> GetCustomMembersFromMetadata<T>(IDataObjectInfo instance,
                                                                  string metadataName)
        where T : class
        => instance
            .Metadata
            .Where(md => md.Name == metadataName)
            .Select(md => md.Value)
            .OfType<T>();

    private static IEnumerable<AttributeBuilder> GetClassAttributeBuilderAttributes(this IDataObjectInfo instance,
                                                                    RenderMetadataAsAttributesTypes renderMetadataAsAttributes,
                                                                    string attributeName)
    {
        if (renderMetadataAsAttributes.HasFlag(RenderMetadataAsAttributesTypes.Custom))
        {
            return instance.Metadata.GetValues<IAttribute>(attributeName).Select(x => new AttributeBuilder(x));
        }

        return Enumerable.Empty<AttributeBuilder>();
    }
}
