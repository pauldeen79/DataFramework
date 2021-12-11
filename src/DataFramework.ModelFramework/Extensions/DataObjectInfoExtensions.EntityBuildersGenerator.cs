using System.Collections.Generic;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;
using ModelFramework.Objects.Extensions;
using ModelFramework.Objects.Settings;

namespace DataFramework.ModelFramework.Extensions
{
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
                .ToImmutableBuilderClassBuilder(new ImmutableBuilderClassSettings(addCopyConstructor: true,
                                                                                  poco: entityClassType.HasPropertySetter(),
                                                                                  addNullChecks: settings.EnableNullableContext))
                .WithNamespace(instance.GetEntityBuildersNamespace())
                .ClearAttributes()
                .AddAttributes(instance.GetEntityBuilderClassAttributes(renderMetadataAsAttributes));
        }

        private static IEnumerable<AttributeBuilder> GetEntityBuilderClassAttributes(this IDataObjectInfo instance,
                                                                                     RenderMetadataAsAttributesTypes renderMetadataAsAttributes)
        {
            yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.Entities.EntityBuilderGenerator");

            if (renderMetadataAsAttributes.HasFlag(RenderMetadataAsAttributesTypes.Custom))
            {
                foreach (var attributeBuilder in instance.GetClassAttributes(renderMetadataAsAttributes, Builders.Attribute))
                {
                    yield return attributeBuilder;
                }
            }
        }
    }
}
