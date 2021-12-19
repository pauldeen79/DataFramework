using System;
using System.Collections.Generic;
using System.Linq;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        public static IClass ToEntityIdentityClass(this IDataObjectInfo instance, GeneratorSettings settings)
            => instance.ToEntityIdentityClassBuilder(settings).Build();

        public static ClassBuilder ToEntityIdentityClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
        {
            var entityClassType = instance.GetEntityClassType(settings.DefaultEntityClassType);
            var renderMetadataAsAttributes = instance.GetRenderMetadataAsAttributesType(settings.DefaultRenderMetadataAsAttributes);

            return new ClassBuilder()
                .WithName(instance.Name + "Identity")
                .WithNamespace(instance.GetEntityIdentitiesNamespace())
                .FillFrom(instance)
                .WithVisibility(instance.Metadata.GetValue(Entities.Visibility, () => instance.IsVisible.ToVisibility()))
                .WithRecord(entityClassType == EntityClassType.Record)
                .AddInterfaces(GetEntityIdentityClassTypeInterfaces(instance, entityClassType))
                .AddProperties(GetEntityIdentityClassProperties(instance, renderMetadataAsAttributes, entityClassType))
                .AddMethods(GetEntityIdentityClassMethods(instance, entityClassType))
                .AddConstructors(GetEntityIdentityClassConstructors(instance, entityClassType, settings))
                .AddAttributes(GetEntityIdentityClassAttributes(instance, renderMetadataAsAttributes));
        }

        private static IEnumerable<string> GetEntityIdentityClassTypeInterfaces(IDataObjectInfo instance,
                                                                                EntityClassType entityClassType)
        {
            if (entityClassType == EntityClassType.ImmutableClass)
            {
                yield return $"IEquatable<{instance.Name}Identity>";
            }
        }

        private static IEnumerable<AttributeBuilder> GetEntityIdentityClassAttributes(IDataObjectInfo instance,
                                                                                      RenderMetadataAsAttributesTypes renderMetadataAsAttributes)
        {
            yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.Entities.EntityIdentityGenerator");

            foreach (var attributeBuilder in instance.GetClassAttributeBuilderAttributes(renderMetadataAsAttributes, Identities.Attribute))
            {
                yield return attributeBuilder;
            }
        }

        private static IEnumerable<ClassMethodBuilder> GetEntityIdentityClassMethods(IDataObjectInfo instance,
                                                                                     EntityClassType entityClassType)
            => ClassMethods(instance,
                            $"{instance.Name}Identity",
                            GetIdentityEntityEqualsProperties(instance),
                            instance.GetIdentityFields(),
                            entityClassType);
        
        private static IEnumerable<ClassConstructorBuilder> GetEntityIdentityClassConstructors(IDataObjectInfo instance,
                                                                                               EntityClassType entityClassType,
                                                                                               GeneratorSettings settings)
        {
            yield return new ClassConstructorBuilder().AddParameter("instance", instance.GetEntityFullName())
                                                      .AddLiteralCodeStatements(instance.GetIdentityFields().Select(x => $"{x.Name.Sanitize()} = instance.{x.CreatePropertyName(instance)};"))
                                                      .AddLiteralCodeStatements(GetValidationCodeStatements(settings));

            if (!entityClassType.IsImmutable())
            {
                yield break;
            }

            yield return new ClassConstructorBuilder()
                .AddParameters(instance.GetIdentityFields().Select(f => f.ToParameterBuilder()))
                .AddLiteralCodeStatements(instance.GetIdentityFields().Select(x => $"{x.Name.Sanitize()} = {x.Name.Sanitize().ToPascalCase()};"))
                .AddLiteralCodeStatements(GetValidationCodeStatements(settings));
        }

        private static string GetIdentityEntityEqualsProperties(IDataObjectInfo instance)
            => string.Join(" &&"
                + Environment.NewLine
                + "       ", instance.GetIdentityFields().Select(f => $"{f.Name.Sanitize()} == other.{f.Name.Sanitize()}"));

        private static IEnumerable<string> GetValidationCodeStatements(GeneratorSettings settings)
        {
            if (settings.AddValidationCodeInConstructor)
            {
                yield return "System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);";
            }
        }

        private static IEnumerable<ClassPropertyBuilder> GetEntityIdentityClassProperties
        (
            IDataObjectInfo instance,
            RenderMetadataAsAttributesTypes renderMetadataAsAttributes,
            EntityClassType entityClassType
        )
        {
            return instance.GetIdentityFields()
                .Select(field => new ClassPropertyBuilder()
                    .WithName(field.Name)
                    .Fill(field)
                    .WithHasSetter(entityClassType.HasPropertySetter())
                    .AddGetterCodeStatements(GetGetterCodeStatements(field, entityClassType))
                    .AddSetterCodeStatements(GetSetterCodeStatements(field, entityClassType))
                    .AddAttributes(GetEntityClassPropertyAttributes(field, instance.Name, entityClassType, renderMetadataAsAttributes, false)));
        }
    }
}
