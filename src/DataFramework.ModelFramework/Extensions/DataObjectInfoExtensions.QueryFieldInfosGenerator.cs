namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoExtensions
{
    public static IClass ToQueryFieldInfoClass(this IDataObjectInfo instance, GeneratorSettings settings)
        => instance.ToQueryFieldInfoClassBuilder(settings).Build();

    public static ClassBuilder ToQueryFieldInfoClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
    {
        var renderMetadataAsAttributes = instance.GetRenderMetadataAsAttributesType(settings.DefaultRenderMetadataAsAttributes);

        return new ClassBuilder()
            .WithName($"{instance.Name}QueryFieldInfo")
            .WithNamespace(instance.GetQueryFieldProvidersNamespace())
            .AddInterfaces(typeof(IQueryFieldInfo))
            .FillFrom(instance)
            .WithVisibility(instance.Metadata.GetValue(QueryFieldInfos.Visibility, () => instance.IsVisible.ToVisibility()))
            .AddAttributes(GetQueryFieldProviderClassAttributes(instance, renderMetadataAsAttributes))
            .AddFields(GetQueryFieldProviderClassFields(instance))
            .AddConstructors(GetQueryFieldProviderClassConstructors(instance))
            .AddMethods(GetQueryFieldProviderClassMethods(instance));
    }

    private static IEnumerable<AttributeBuilder> GetQueryFieldProviderClassAttributes(IDataObjectInfo instance, RenderMetadataAsAttributesTypes renderMetadataAsAttributes)
    {
        yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.Queries.QueryFieldInfoGenerator");

        foreach (var attribute in instance.GetClassAttributeBuilderAttributes(renderMetadataAsAttributes, QueryFieldInfos.Attribute))
        {
            yield return attribute;
        }
    }

    private static IEnumerable<ClassFieldBuilder> GetQueryFieldProviderClassFields(IDataObjectInfo instance)
        => instance.Metadata.GetValues<IClassField>(QueryFieldInfos.Field).Select(x => new ClassFieldBuilder(x));

    private static IEnumerable<ClassConstructorBuilder> GetQueryFieldProviderClassConstructors(IDataObjectInfo instance)
    {
        var constructorParameters = instance.Metadata.GetValues<IParameter>(QueryFieldInfos.ConstructorParameter).Select(x => new ParameterBuilder(x)).ToArray();
        var constructorStatements = instance.Metadata.GetValues<ICodeStatement>(QueryFieldInfos.ConstructorCodeStatement).Select(x => x.CreateBuilder()).ToArray();
        if (constructorParameters.Any() || constructorStatements.Any())
        {
            yield return new ClassConstructorBuilder()
                .AddParameters(constructorParameters)
                .AddCodeStatements(constructorStatements);
        }
    }

    private static IEnumerable<ClassMethodBuilder> GetQueryFieldProviderClassMethods(IDataObjectInfo instance)
    {
        yield return new ClassMethodBuilder()
            .WithName(nameof(IQueryFieldInfo.GetAllFields))
            .WithType(typeof(IEnumerable<string>))
            .AddLiteralCodeStatements(instance.Fields.Where(x => x.UseOnSelect() && !x.Metadata.GetBooleanValue(QueryFieldInfos.SkipField)).Select(x => $"yield return {x.CreatePropertyName(instance).CsharpFormat()};"))
            .AddCodeStatements(instance.Metadata.GetValues<ICodeStatement>(QueryFieldInfos.GetAllFieldsCodeStatement).Select(x => x.CreateBuilder()));

        yield return new ClassMethodBuilder()
            .WithName(nameof(IQueryFieldInfo.GetDatabaseFieldName))
            .AddParameter("queryFieldName", typeof(string))
            .WithType(typeof(string))
            .WithIsNullable()
            .AddCodeStatements(instance.Metadata.GetValues<ICodeStatement>(QueryFieldInfos.GetDatabaseFieldNameCodeStatement).Select(x => x.CreateBuilder()))
            .AddLiteralCodeStatements($"return GetAllFields().FirstOrDefault(x => x.Equals(queryFieldName, {nameof(StringComparison)}.{nameof(StringComparison.OrdinalIgnoreCase)}));");
    }
}
