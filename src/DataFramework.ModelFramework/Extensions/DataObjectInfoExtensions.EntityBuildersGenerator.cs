using System.Collections.Generic;
using System.Linq;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        public static ClassBuilder ToEntityBuilderClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
        {
            var entityClassType = instance.GetEntityClassType(settings.DefaultEntityClassType);
            var renderMetadataAsAttributes = instance.GetRenderMetadataAsAttributesType(settings.DefaultRenderMetadataAsAttributes);

            return new ClassBuilder()
                .WithName($"{instance.Name}Builder")
                .WithNamespace(instance.GetEntityBuildersNamespace())
                .FillFrom(instance)
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

        private static IEnumerable<ClassMethodBuilder> GetEntityBuilderClassMethods(IDataObjectInfo instance,
                                                                                    EntityClassType entityClassType)
        {
            foreach (var field in GetFieldsWithConcurrencyCheckFields(instance))
            {
                yield return new ClassMethodBuilder()
                    .WithName($"Set{field.Name}")
                    .WithTypeName($"{instance.Name}Builder")
                    .AddLiteralCodeStatements
                    (
                        $"this.{field.Name.Sanitize()} = value;",
                        "return this;"
                    )
                    .AddParameters(new ParameterBuilder().WithName("value")
                                                         .WithTypeName(field.TypeName)
                                                         .WithIsNullable(field.IsNullable));
            }

            yield return new ClassMethodBuilder()
                .WithName("Build")
                .WithTypeName(instance.GetEntityFullName())
                .AddLiteralCodeStatements(entityClassType.HasPropertySetter()
                    ? $"return new {instance.GetEntityFullName()} {{ {string.Join(", ", GetFieldsWithConcurrencyCheckFields(instance).Select(f => $"{f.Name.Sanitize()} = {f.Name.Sanitize()}"))} }};"
                    : $"return new {instance.GetEntityFullName()}({string.Join(", ", GetFieldsWithConcurrencyCheckFields(instance).Select(f => f.Name.Sanitize()))});");

            yield return new ClassMethodBuilder()
                .WithName("Update")
                .WithTypeName($"{instance.Name}Builder")
                .AddParameter("instance", $"{instance.GetEntityFullName()}")
                .AddLiteralCodeStatements(GetFieldsWithConcurrencyCheckFields(instance)
                    .Select(field => $"this.{field.Name.Sanitize()} = instance.{field.CreatePropertyName(instance)};"))
                .AddLiteralCodeStatements("return this;");
        }

        private static IEnumerable<ClassConstructorBuilder> GetEntityBuilderClassConstructors(IDataObjectInfo instance,
                                                                                              EntityClassType entityClassType)
        {
            if (!entityClassType.IsImmutable())
            {
                yield break;
            }

            yield return new ClassConstructorBuilder();

            yield return new ClassConstructorBuilder()
                .AddParameter("instance", instance.GetEntityFullName())
                .AddLiteralCodeStatements(GetFieldsWithConcurrencyCheckFields(instance)
                    .Select(x => $"{x.Name.Sanitize()} = instance.{x.CreatePropertyName(instance)};"));

            yield return new ClassConstructorBuilder()
                .AddLiteralCodeStatements(GetEntityClassConstructorCodeStatements(instance, RenderMetadataAsAttributesType.None, false)) //RenderMetaDataAsAttributesType.None skips validation call
                .AddParameters(GetFieldsWithConcurrencyCheckFields(instance).Select(f => f.ToParameterBuilder()));
        }

        private static IEnumerable<ClassPropertyBuilder> GetEntityBuilderClassProperties(IDataObjectInfo instance,
                                                                                         RenderMetadataAsAttributesType renderMetadataAsAttributes,
                                                                                         EntityClassType entityClassType)
            => instance.Fields.Select(field =>
                    new ClassPropertyBuilder()
                        .WithName(field.Name)
                        .WithTypeName(field.Metadata.GetMetadataStringValue(Entities.PropertyType, field.TypeName ?? string.Empty))
                        .Fill(field)
                        .AddGetterCodeStatements(GetGetterCodeStatements(field, entityClassType, true))
                        .AddSetterCodeStatements(GetSetterCodeStatements(field, entityClassType, true))
                        .AddAttributes(GetEntityClassPropertyAttributes(field, instance.Name, entityClassType, renderMetadataAsAttributes, true, false)))
                .Concat(instance.GetUpdateConcurrencyCheckFields().Select(field =>
                    new ClassPropertyBuilder()
                        .WithName($"{field.Name}Original")
                        .Fill(field)
                        .WithIsNullable()
                        .AddGetterCodeStatements(GetGetterCodeStatements(field, entityClassType, true))
                        .AddSetterCodeStatements(GetSetterCodeStatements(field, entityClassType, true))));
    }
}
