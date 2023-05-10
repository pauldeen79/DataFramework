namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoExtensions
{
    public static IClass ToPagedEntityRetrieverSettingsClass(this IDataObjectInfo instance, GeneratorSettings settings)
        => instance.ToPagedEntityRetrieverSettingsClassBuilder(settings).BuildTyped();

    public static ClassBuilder ToPagedEntityRetrieverSettingsClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
        => new ClassBuilder()
            .WithName($"{instance.Name}PagedEntityRetrieverSettings")
            .WithNamespace(instance.GetPagedEntityRetrieverSettingsNamespace())
            .FillFrom(instance)
            .WithVisibility(instance.Metadata.GetValue(PagedEntityRetrieverSettings.Visibility, () => instance.IsVisible.ToVisibility()))
            .AddInterfaces(typeof(IPagedDatabaseEntityRetrieverSettings))
            .AddAttributes(GetPagedEntityRetrieverSettingsClassAttributes(instance))
            .AddProperties(GetPagedEntityRetrieverSettingsClassProperties(instance));

    private static IEnumerable<AttributeBuilder> GetPagedEntityRetrieverSettingsClassAttributes(IDataObjectInfo instance)
    {
        yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.PagedEntityRetrieverSettingsGenerator");

        foreach (var attribute in instance.Metadata.GetValues<IAttribute>(PagedEntityRetrieverSettings.Attribute))
        {
            yield return new AttributeBuilder(attribute);
        }
    }

    private static IEnumerable<ClassPropertyBuilder> GetPagedEntityRetrieverSettingsClassProperties(IDataObjectInfo instance)
    {
        yield return new ClassPropertyBuilder()
            .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.TableName))
            .WithType(typeof(string))
            .WithHasSetter(false)
            .AddGetterLiteralCodeStatements($"return {instance.Metadata.GetStringValue(PagedEntityRetrieverSettings.TableName, () => instance.GetTableName()).CsharpFormat()};");

        yield return new ClassPropertyBuilder()
            .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.Fields))
            .WithType(typeof(string))
            .WithHasSetter(false)
            .AddGetterLiteralCodeStatements(string.Concat
            (
                "return ",
                string.Join(", ", instance.Fields.Where(x => x.UseOnSelect()).Select(x => x.GetDatabaseFieldName()).Concat(instance.Metadata.GetStringValues(PagedEntityRetrieverSettings.Field))).CsharpFormat(),
                ";"
            ));

        yield return new ClassPropertyBuilder()
            .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.DefaultOrderBy))
            .WithType(typeof(string))
            .WithHasSetter(false)
            .AddGetterLiteralCodeStatements($"return {instance.Metadata.GetStringValue(PagedEntityRetrieverSettings.DefaultOrderByFields).CsharpFormat()};");

        yield return new ClassPropertyBuilder()
            .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.DefaultWhere))
            .WithType(typeof(string))
            .WithHasSetter(false)
            .AddGetterLiteralCodeStatements($"return {instance.Metadata.GetStringValue(PagedEntityRetrieverSettings.DefaultWhereClause).CsharpFormat()};");

        yield return new ClassPropertyBuilder()
            .WithName(nameof(IPagedDatabaseEntityRetrieverSettings.OverridePageSize))
            .WithType(typeof(int?))
            .WithHasSetter(false)
            .AddGetterLiteralCodeStatements($"return {instance.Metadata.GetValue<int?>(PagedEntityRetrieverSettings.OverridePageSize, () => -1)};");
    }
}
