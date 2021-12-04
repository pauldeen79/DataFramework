using System.Collections.Generic;
using System.Linq;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.CodeStatements;
using ModelFramework.Objects.Default;
using MFAttribute = ModelFramework.Objects.Default.Attribute;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        public static ClassBuilder ToEntityBuilderClass
        (
            this IDataObjectInfo instance,
            RenderMetadataAsAttributesType defaultRenderMetadataAsAttributes = RenderMetadataAsAttributesType.Validation,
            EntityClassType defaultEntityClassType = EntityClassType.Poco
        )
        {
            var entityClassType = instance.Metadata.GetMetadataValue<EntityClassType?>(Entities.EntityClassType, null) ?? defaultEntityClassType;
            var renderMetadataAsAttributes = instance.Metadata.GetMetadataValue<RenderMetadataAsAttributesType?>(Entities.RenderMetadataAsAttributesType, null) ?? defaultRenderMetadataAsAttributes;

            return new ClassBuilder()
                .WithName(instance.Name + "Builder")
                .WithNamespace(instance.Metadata.GetMetadataStringValue(Entities.BuildersNamespace).WhenNullOrEmpty(() => instance.Metadata.GetMetadataStringValue(Entities.Namespace, instance.TypeName?.GetNamespaceWithDefault(string.Empty) ?? string.Empty)))
                .WithVisibility(instance.Metadata.GetMetadataValue(Entities.Visibility, instance.IsVisible.ToVisibility()))
                .WithPartial()
                .AddProperties(GetEntityBuilderClassProperties(instance, renderMetadataAsAttributes, entityClassType))
                .AddMethods(GetEntityBuilderClassMethods(instance, entityClassType))
                .AddConstructors(GetEntityBuilderClassConstructors(instance, entityClassType))
                .AddMetadata(instance.Metadata.Convert())
                .AddAttributes(GetEntityBuilderClassAttributes(instance, renderMetadataAsAttributes));
        }

        private static IEnumerable<AttributeBuilder> GetEntityBuilderClassAttributes(IDataObjectInfo instance,
                                                                                     RenderMetadataAsAttributesType renderMetadataAsAttributes)
        {
            var result = new List<AttributeBuilder>
            {
                new AttributeBuilder(new MFAttribute
                (
                    "System.CodeDom.Compiler.GeneratedCode",
                    new[]
                    {
                        new AttributeParameter("DataFramework.ModelFramework.Generators.Entities.EntityBuilderGenerator"),
                        new AttributeParameter(typeof(DataObjectInfoExtensions).Assembly.GetName().Version.ToString())
                    }
                ))
            };

            return instance.AddClassAttributes(renderMetadataAsAttributes, Entities.EntityBuildersAttribute, result);
        }

        private static IEnumerable<ClassMethodBuilder> GetEntityBuilderClassMethods(IDataObjectInfo instance, EntityClassType entityClassType)
        {
            var ns = instance.Metadata.GetMetadataStringValue(Entities.BuildersNamespace).WhenNullOrEmpty(() => instance.Metadata.GetMetadataStringValue(Entities.Namespace, instance.TypeName?.GetNamespaceWithDefault(string.Empty) ?? string.Empty));
            foreach (var field in GetFieldsWithConcurrencyCheckFields(instance))
            {
                yield return new ClassMethodBuilder()
                    .WithName($"Set{field.Name.Sanitize()}")
                    .WithTypeName($"{ns}.{instance.Name}Builder")
                    .AddCodeStatements
                    (
                        new LiteralCodeStatement($"this.{field.Name.Sanitize()} = value;"),
                        new LiteralCodeStatement("return this;")
                    )
                    .AddParameters(new Parameter("value", field.TypeName, isNullable: field.IsNullable));
            }

            yield return new ClassMethodBuilder()
                .WithName("Build")
                .WithTypeName($"{ns}.{instance.Name}")
                .AddCodeStatements(entityClassType == EntityClassType.Poco || entityClassType == EntityClassType.ObservablePoco
                    ? new LiteralCodeStatement($"return new {ns}.{instance.Name} {{ {string.Join(", ", GetFieldsWithConcurrencyCheckFields(instance).Select(f => $"{f.Name.Sanitize()} = {f.Name.Sanitize()}"))} }};")
                    : new LiteralCodeStatement($"return new {ns}.{instance.Name}({string.Join(", ", GetFieldsWithConcurrencyCheckFields(instance).Select(f => f.Name.Sanitize()))});"));

            yield return new ClassMethodBuilder()
                .WithName("Update")
                .WithTypeName($"{ns}.{instance.Name}Builder")
                .AddParameters(new Parameter("instance", $"{ns}.{instance.Name}"))
                .AddCodeStatements(GetFieldsWithConcurrencyCheckFields(instance).Select(field => new LiteralCodeStatement($"this.{field.Name} = instance.{field.CreatePropertyName(instance)};")).Concat(new[] { new LiteralCodeStatement("return this;") }));
        }

        private static IEnumerable<ClassConstructorBuilder> GetEntityBuilderClassConstructors(IDataObjectInfo instance,
                                                                                              EntityClassType entityClassType)
        {
            if (entityClassType.In(EntityClassType.ImmutableClass, EntityClassType.Record))
            {
                var ns = instance.Metadata.GetMetadataStringValue(Entities.BuildersNamespace).WhenNullOrEmpty(() => instance.Metadata.GetMetadataStringValue(Entities.Namespace, instance.TypeName?.GetNamespaceWithDefault(string.Empty) ?? string.Empty));
                yield return new ClassConstructorBuilder();

                yield return new ClassConstructorBuilder()
                    .AddParameters(new Parameter("instance", $"{ns}.{instance.Name}"))
                    .AddCodeStatements(GetFieldsWithConcurrencyCheckFields(instance).Select(x => new LiteralCodeStatement($"{x.Name} = instance.{x.CreatePropertyName(instance)};")));

                yield return new ClassConstructorBuilder()
                    .AddCodeStatements(GetEntityClassConstructorCodeStatements(instance, entityClassType, RenderMetadataAsAttributesType.None, false)) //RenderMetaDataAsAttributesType.None skips validation call
                    .AddParameters(GetFieldsWithConcurrencyCheckFields(instance).Select(f => new Parameter(f.Name.ToPascalCase(), f.TypeName, f.DefaultValue, f.IsNullable)));
            }
        }

        private static IEnumerable<ClassPropertyBuilder> GetEntityBuilderClassProperties(IDataObjectInfo instance,
                                                                                         RenderMetadataAsAttributesType renderMetadataAsAttributes,
                                                                                         EntityClassType entityClassType)
        {
            var result = new List<ClassPropertyBuilder>();

            //Add properties for all instance.Fields
            foreach (var field in instance.Fields)
            {
                result.Add(new ClassPropertyBuilder()
                    .WithName(field.Name)
                    .WithTypeName(field.Metadata.GetMetadataStringValue(Entities.PropertyType, field.TypeName ?? string.Empty))
                    .WithIsNullable(field.IsNullable)
                    .WithStatic(field.Metadata.GetMetadataStringValue(Entities.Static).IsTrue())
                    .WithVirtual(field.Metadata.GetMetadataStringValue(Entities.Virtual).IsTrue())
                    .WithAbstract(field.Metadata.GetMetadataStringValue(Entities.Abstract).IsTrue())
                    .WithProtected(field.Metadata.GetMetadataStringValue(Entities.Protected).IsTrue())
                    .WithOverride(field.Metadata.GetMetadataStringValue(Entities.Override).IsTrue())
                    .WithVisibility(field.Metadata.GetMetadataValue(Entities.Visibility, field.IsVisible.ToVisibility()))
                    .WithGetterVisibility(field.Metadata.GetMetadataValue(global::ModelFramework.Objects.MetadataNames.PropertyGetterVisibility, field.IsVisible.ToVisibility()))
                    .WithSetterVisibility(field.Metadata.GetMetadataValue(global::ModelFramework.Objects.MetadataNames.PropertySetterVisibility, field.IsVisible.ToVisibility()))
                    .AddGetterCodeStatements(GetCodeStatements(field, entityClassType, Entities.PropertyGetterCodeStatement, string.Empty))
                    .AddSetterCodeStatements(GetCodeStatements(field, entityClassType, Entities.PropertySetterCodeStatement, string.Empty))
                    .AddMetadata(field.Metadata.Convert())
                    .AddAttributes(GetEntityClassPropertyAttributes(field,
                                                                    instance.Name,
                                                                    renderMetadataAsAttributes,
                                                                    true)));
            }

            foreach (var field in instance.GetUpdateConcurrencyCheckFields())
            {
                result.Add(new ClassPropertyBuilder()
                    .WithName($"{field.Name}Original")
                    .WithTypeName(field.Metadata.GetMetadataStringValue(Entities.PropertyType, field.TypeName ?? string.Empty))
                    .WithIsNullable(field.IsNullable)
                    .WithStatic(field.Metadata.GetMetadataStringValue(Entities.Static).IsTrue())
                    .WithVirtual(field.Metadata.GetMetadataStringValue(Entities.Virtual).IsTrue())
                    .WithAbstract(field.Metadata.GetMetadataStringValue(Entities.Abstract).IsTrue())
                    .WithProtected(field.Metadata.GetMetadataStringValue(Entities.Protected).IsTrue())
                    .WithOverride(field.Metadata.GetMetadataStringValue(Entities.Override).IsTrue())
                    .WithVisibility(field.Metadata.GetMetadataValue(Entities.Visibility, field.IsVisible.ToVisibility()))
                    .WithGetterVisibility(field.Metadata.GetMetadataValue(global::ModelFramework.Objects.MetadataNames.PropertyGetterVisibility, field.IsVisible.ToVisibility()))
                    .WithSetterVisibility(field.Metadata.GetMetadataValue(global::ModelFramework.Objects.MetadataNames.PropertySetterVisibility, field.IsVisible.ToVisibility()))
                    .AddGetterCodeStatements(GetCodeStatements(field, entityClassType, Entities.PropertyGetterCodeStatement, string.Empty))
                    .AddSetterCodeStatements(GetCodeStatements(field, entityClassType, Entities.PropertySetterCodeStatement, string.Empty))
                    .AddMetadata(field.Metadata.Convert())
                    .AddAttributes(new MFAttribute("ReadOnly", new[] { new AttributeParameter(true) })));
            }

            return result;
        }
    }
}
