using System.Collections.Generic;
using System.Linq;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.CodeStatements.Builders;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        public static ClassBuilder ToEntityBuilderClassBuilder
        (
            this IDataObjectInfo instance,
            RenderMetadataAsAttributesType defaultRenderMetadataAsAttributes = RenderMetadataAsAttributesType.Validation,
            EntityClassType defaultEntityClassType = EntityClassType.Poco
        )
        {
            var entityClassType = instance.Metadata.GetMetadataValue<EntityClassType?>(Entities.EntityClassType, null) ?? defaultEntityClassType;
            var renderMetadataAsAttributes = instance.Metadata.GetMetadataValue<RenderMetadataAsAttributesType?>(Entities.RenderMetadataAsAttributesType, null) ?? defaultRenderMetadataAsAttributes;

            return new ClassBuilder()
                .WithName($"{instance.Name}Builder")
                .WithNamespace(instance.Metadata.GetMetadataStringValue(Entities.BuildersNamespace)
                    .WhenNullOrEmpty(() => instance.GetEntitiesNamespace()))
                .WithSharedDataObjectInfoData(instance)
                .WithVisibility(instance.Metadata.GetMetadataValue(Entities.Visibility, instance.IsVisible.ToVisibility()))
                .AddProperties(GetEntityBuilderClassProperties(instance, renderMetadataAsAttributes, entityClassType))
                .AddMethods(GetEntityBuilderClassMethods(instance, entityClassType))
                .AddConstructors(GetEntityBuilderClassConstructors(instance, entityClassType))
                .AddAttributes(GetEntityBuilderClassAttributes(instance, renderMetadataAsAttributes));
        }

        private static IEnumerable<AttributeBuilder> GetEntityBuilderClassAttributes(IDataObjectInfo instance,
                                                                                     RenderMetadataAsAttributesType renderMetadataAsAttributes)
        {
            yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.Entities.EntityBuilderGenerator");

            foreach (var attributeBuilder in instance.GetClassAttributes(renderMetadataAsAttributes, Entities.EntityBuildersAttribute))
            {
                yield return attributeBuilder;
            }
        }

        private static IEnumerable<ClassMethodBuilder> GetEntityBuilderClassMethods(IDataObjectInfo instance, EntityClassType entityClassType)
        {
            var ns = instance.GetEntitiesNamespace();
            foreach (var field in GetFieldsWithConcurrencyCheckFields(instance))
            {
                yield return new ClassMethodBuilder()
                    .WithName($"Set{field.Name.Sanitize()}")
                    .WithTypeName($"{ns}.{instance.Name}Builder")
                    .AddCodeStatements
                    (
                        new LiteralCodeStatementBuilder().WithStatement($"this.{field.Name.Sanitize()} = value;"),
                        new LiteralCodeStatementBuilder().WithStatement("return this;")
                    )
                    .AddParameters(new ParameterBuilder().WithName("value")
                                                         .WithTypeName(field.TypeName)
                                                         .WithIsNullable(field.IsNullable));
            }

            yield return new ClassMethodBuilder()
                .WithName("Build")
                .WithTypeName($"{ns}.{instance.Name}")
                .AddCodeStatements(entityClassType.HasPropertySetter()
                    ? new LiteralCodeStatementBuilder()
                        .WithStatement($"return new {ns}.{instance.Name} {{ {string.Join(", ", GetFieldsWithConcurrencyCheckFields(instance).Select(f => $"{f.Name.Sanitize()} = {f.Name.Sanitize()}"))} }};")
                    : new LiteralCodeStatementBuilder()
                        .WithStatement($"return new {ns}.{instance.Name}({string.Join(", ", GetFieldsWithConcurrencyCheckFields(instance).Select(f => f.Name.Sanitize()))});"));

            yield return new ClassMethodBuilder()
                .WithName("Update")
                .WithTypeName($"{ns}.{instance.Name}Builder")
                .AddParameters(new ParameterBuilder().WithName("instance")
                                                     .WithTypeName($"{ns}.{instance.Name}"))
                .AddCodeStatements(GetFieldsWithConcurrencyCheckFields(instance)
                    .Select(field => new LiteralCodeStatementBuilder()
                        .WithStatement($"this.{field.Name} = instance.{field.CreatePropertyName(instance)};")))
                .AddCodeStatements(new LiteralCodeStatementBuilder()
                    .WithStatement("return this;"));
        }

        private static IEnumerable<ClassConstructorBuilder> GetEntityBuilderClassConstructors(IDataObjectInfo instance,
                                                                                              EntityClassType entityClassType)
        {
            if (entityClassType.IsImmutable())
            {
                var ns = instance.GetEntitiesNamespace();
                yield return new ClassConstructorBuilder();

                yield return new ClassConstructorBuilder()
                    .AddParameters(new ParameterBuilder().WithName("instance")
                                                         .WithTypeName($"{ns}.{instance.Name}"))
                    .AddCodeStatements(GetFieldsWithConcurrencyCheckFields(instance)
                        .Select(x => new LiteralCodeStatementBuilder()
                            .WithStatement($"{x.Name} = instance.{x.CreatePropertyName(instance)};")));

                yield return new ClassConstructorBuilder()
                    .AddCodeStatements(GetEntityClassConstructorCodeStatements(instance, entityClassType, RenderMetadataAsAttributesType.None, false)) //RenderMetaDataAsAttributesType.None skips validation call
                    .AddParameters(GetFieldsWithConcurrencyCheckFields(instance).Select(f => f.ToParameterBuilder()));
            }
        }

        private static IEnumerable<ClassPropertyBuilder> GetEntityBuilderClassProperties(IDataObjectInfo instance,
                                                                                         RenderMetadataAsAttributesType renderMetadataAsAttributes,
                                                                                         EntityClassType entityClassType)
            => instance.Fields.Select(field =>
                    new ClassPropertyBuilder()
                        .WithName(field.Name)
                        .WithTypeName(field.Metadata.GetMetadataStringValue(Entities.PropertyType, field.TypeName ?? string.Empty))
                        .WithSharedFieldInfoData(field)
                        .AddGetterCodeStatements(GetGetterCodeStatements(field, entityClassType, true))
                        .AddSetterCodeStatements(GetSetterCodeStatements(field, entityClassType, true))
                        .AddAttributes(GetEntityClassPropertyAttributes(field, instance.Name, entityClassType, renderMetadataAsAttributes, true, false)))
                .Concat(instance.GetUpdateConcurrencyCheckFields().Select(field =>
                    new ClassPropertyBuilder()
                        .WithName($"{field.Name}Original")
                        .WithSharedFieldInfoData(field)
                        .AddGetterCodeStatements(GetGetterCodeStatements(field, entityClassType, true))
                        .AddSetterCodeStatements(GetSetterCodeStatements(field, entityClassType, true))));
    }
}
