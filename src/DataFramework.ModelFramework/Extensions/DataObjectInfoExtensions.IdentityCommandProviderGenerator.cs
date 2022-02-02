namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoExtensions
{
    public static IClass ToIdentityCommandProviderClass(this IDataObjectInfo instance, GeneratorSettings settings)
        => instance.ToIdentityCommandProviderClassBuilder(settings).Build();

    public static ClassBuilder ToIdentityCommandProviderClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
        => new ClassBuilder()
            .WithName($"{instance.Name}IdentityCommandProvider")
            .WithNamespace(instance.GetCommandProvidersNamespace())
            .FillFrom(instance)
            .WithVisibility(instance.Metadata.GetValue(CommandProviders.Visibility, () => instance.IsVisible.ToVisibility()))
            .WithBaseClass(typeof(IdentityDatabaseCommandProviderBase<>).CreateGenericTypeName(instance.GetEntityIdentityFullName()))
            .AddAttributes(GetIdentityCommandProviderClassAttributes(instance))
            .AddConstructors(new ClassConstructorBuilder().WithChainCall($"base(new {instance.GetEntityRetrieverFullName()}())"))
            .AddMethods(new ClassMethodBuilder().WithName("GetFields")
                                                .WithProtected()
                                                .WithOverride()
                                                .WithVisibility(Visibility.Private)
                                                .WithType(typeof(IEnumerable<IdentityDatabaseCommandProviderField>))
                                                .AddParameter("source", instance.GetEntityIdentityFullName())
                                                .AddParameter("operation", typeof(DatabaseOperation))
                                                .AddLiteralCodeStatements(instance.GetIdentityFields().Select(x => $"yield return new {nameof(IdentityDatabaseCommandProviderField)}({x.CreatePropertyName(instance).CsharpFormat()}, {x.GetDatabaseFieldName().CsharpFormat()});")));

    private static IEnumerable<AttributeBuilder> GetIdentityCommandProviderClassAttributes(IDataObjectInfo instance)
    {
        yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.IdentityCommandProviderGenerator");

        foreach (var attribute in instance.Metadata.GetValues<IAttribute>(CommandProviders.Attribute))
        {
            yield return new AttributeBuilder(attribute);
        }
    }
}
