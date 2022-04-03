namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoExtensions
{
    public static IClass ToDatabaseEntityRetrieverProviderClass(this IDataObjectInfo instance, GeneratorSettings settings)
        => instance.ToDatabaseEntityRetrieverProviderClassBuilder(settings).Build();

    public static ClassBuilder ToDatabaseEntityRetrieverProviderClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
        => new ClassBuilder()
            .WithName($"{instance.Name}EntityRetrieverProvider")
            .WithNamespace(instance.GetCommandEntityProvidersNamespace())
            .FillFrom(instance)
            .WithVisibility(instance.Metadata.GetValue(EntityRetrieverProvider.Visibility, () => instance.IsVisible.ToVisibility()))
            .AddInterfaces(typeof(IDatabaseEntityRetrieverProvider))
            .AddAttributes(GetEntityRetrieverProviderClassAttributes(instance))
            .AddFields(GetEntityRetrieverProviderClassFields(instance))
            .AddMethods(GetEntityRetrieverProviderClassMethods(instance))
            .AddConstructors(GetEntityRetrieverProviderClassConstructors(instance));

    private static IEnumerable<AttributeBuilder> GetEntityRetrieverProviderClassAttributes(IDataObjectInfo instance)
    {
        yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.EntityRetrieverProviderGenerator");

        foreach (var attribute in instance.Metadata.GetValues<IAttribute>(EntityRetrieverProvider.Attribute))
        {
            yield return new AttributeBuilder(attribute);
        }
    }

    private static IEnumerable<ClassFieldBuilder> GetEntityRetrieverProviderClassFields(IDataObjectInfo instance)
    {
        yield return new ClassFieldBuilder()
            .WithName("_databaseEntityRetriever")
            .WithReadOnly()
            .WithTypeName(typeof(IDatabaseEntityRetriever<>).CreateGenericTypeName(instance.GetEntityFullName()));
    }

    private static IEnumerable<ClassMethodBuilder> GetEntityRetrieverProviderClassMethods(IDataObjectInfo instance)
    {
        yield return new ClassMethodBuilder()
            .WithName("TryCreate")
            .WithType(typeof(bool))
            .AddParameters
            (
                new ParameterBuilder()
                    .WithName("query")
                    .WithType(typeof(ISingleEntityQuery)),
                new ParameterBuilder()
                    .WithName("result")
                    .WithIsNullable()
                    .WithTypeName($"out {typeof(IDatabaseEntityRetriever<>).CreateGenericTypeName("TResult")}")
            )
            .AddLiteralCodeStatements
            (
                $"if (typeof(TResult) == typeof({instance.GetEntityFullName()})",
                "{",
                $"    result = ({typeof(IDatabaseEntityRetriever<>).CreateGenericTypeName(instance.GetEntityFullName())})_databaseEntityRetriever;",
                "    return true;",
                "}",
                "result = default;",
                "return false;"
            )
            .AddGenericTypeArguments("TResult")
            .AddGenericTypeArgumentConstraints("where TResult : class");
    }

    private static IEnumerable<ClassConstructorBuilder> GetEntityRetrieverProviderClassConstructors(IDataObjectInfo instance)
    {
        yield return new ClassConstructorBuilder()
            .AddParameter("databaseEntityRetriever", typeof(IDatabaseEntityRetriever<>).CreateGenericTypeName(instance.GetEntityFullName()))
            .AddLiteralCodeStatements("_databaseEntityRetriever = databaseEntityRetriever;");
    }
}
