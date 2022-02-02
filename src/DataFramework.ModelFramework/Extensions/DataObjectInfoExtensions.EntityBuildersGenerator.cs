namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoExtensions
{
    public static IClass ToEntityBuilderClass(this IDataObjectInfo instance, GeneratorSettings settings)
        => instance.ToEntityBuilderClassBuilder(settings).Build();

    public static ClassBuilder ToEntityBuilderClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
    {
        var entityClassType = instance.GetEntityClassType(settings.DefaultEntityClassType);
        var renderMetadataAsAttributes = instance.GetRenderMetadataAsAttributesType(settings.DefaultRenderMetadataAsAttributes);

        return instance
            .ToEntityClass(settings)
            .Chain(x =>
            {
                x.Properties.Select
                (p => new
                {
                    Property = p,
                    FieldInfo = instance.Fields.FirstOrDefault(f => f.Name == p.Name || $"{f.Name}Original" == p.Name)
                }
                )
                .Where(x => x.FieldInfo != null && (x.FieldInfo.IsComputed || !x.FieldInfo.CanSet))
                .ToList()
                .ForEach(y => x.Properties.Remove(y.Property));
            })
            .ToImmutableBuilderClassBuilder(new ImmutableBuilderClassSettings(constructorSettings: new ImmutableBuilderClassConstructorSettings(addCopyConstructor: true),
                                                                              poco: entityClassType.HasPropertySetter(),
                                                                              addNullChecks: !settings.EnableNullableContext))
            .WithNamespace(instance.GetEntityBuildersNamespace())
            .Chain(x => x.Attributes.Clear())
            .AddAttributes(instance.GetEntityBuilderClassAttributes(renderMetadataAsAttributes));
    }

    private static IEnumerable<AttributeBuilder> GetEntityBuilderClassAttributes(this IDataObjectInfo instance,
                                                                                 RenderMetadataAsAttributesTypes renderMetadataAsAttributes)
    {
        yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.Entities.EntityBuilderGenerator");

        foreach (var attributeBuilder in instance.GetClassAttributeBuilderAttributes(renderMetadataAsAttributes, Builders.Attribute))
        {
            yield return attributeBuilder;
        }
    }
}
