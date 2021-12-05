using System;
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
        public static ClassBuilder ToEntityIdentityClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
        {
            var entityClassType = instance.GetEntityClassType(settings.DefaultEntityClassType);
            var renderMetadataAsAttributes = instance.GetRenderMetadataAsAttributesType(settings.DefaultRenderMetadataAsAttributes);

            return new ClassBuilder()
                .WithName(instance.Name + "Identity")
                .WithNamespace(instance.GetEntityIdentitiesNamespace())
                .FillFrom(instance)
                .WithRecord(entityClassType == EntityClassType.Record)
                .AddInterfaces(GetEntityIdentityClassTypeInterfaces(instance, entityClassType))
                .AddProperties(GetEntityIdentityClassProperties(instance, renderMetadataAsAttributes, entityClassType))
                .AddMethods(GetEntityIdentityClassMethods(instance, entityClassType))
                .AddConstructors(GetEntityIdentityClassConstructors(instance))
                .AddAttributes(GetEntityIdentityClassAttributes(instance, renderMetadataAsAttributes));
        }

        private static IEnumerable<string> GetEntityIdentityClassTypeInterfaces(IDataObjectInfo instance,
                                                                                EntityClassType entityClassType)
        {
            if (entityClassType == EntityClassType.ImmutableClass)
            {
                yield return $"IEquatable<{instance.Name}>";
            }
        }

        private static IEnumerable<AttributeBuilder> GetEntityIdentityClassAttributes(IDataObjectInfo instance,
                                                                                      RenderMetadataAsAttributesType renderMetadataAsAttributes)
        {
            yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.Entities.EntityIdentityGenerator");

            foreach (var attributeBuilder in instance.GetClassAttributes(renderMetadataAsAttributes, Entities.EntityBuildersAttribute))
            {
                yield return attributeBuilder;
            }
        }

        private static IEnumerable<ClassMethodBuilder> GetEntityIdentityClassMethods(IDataObjectInfo instance, EntityClassType entityClassType)
            => ClassMethods(instance,
                            $"{instance.Name}Identity",
                            GetIdentityEntityEqualsProperties(instance),
                            instance.Fields.Where(f => f.IsIdentityField && !f.SkipFieldOnFind()),
                            entityClassType);
        
        private static IEnumerable<ClassConstructorBuilder> GetEntityIdentityClassConstructors(IDataObjectInfo instance)
        {
            yield return new ClassConstructorBuilder().AddParameter("instance", instance.GetEntityFullName())
                                                      .AddLiteralCodeStatements(instance.Fields.Where(f => f.IsIdentityField && !f.SkipFieldOnFind()).Select(x => $"{x.Name.Sanitize()} = instance.{x.CreatePropertyName(instance)};"))
                                                      .AddLiteralCodeStatements(GetValidationCodeStatements());

            yield return new ClassConstructorBuilder()
                .AddParameters(instance.Fields.Where(f => f.IsIdentityField && !f.SkipFieldOnFind()).Select(f => f.ToParameterBuilder()))
                .AddLiteralCodeStatements(instance.Fields.Where(f => f.IsIdentityField && !f.SkipFieldOnFind()).Select(x => $"{x.Name.Sanitize()} = {x.Name.Sanitize().ToPascalCase()};"))
                .AddLiteralCodeStatements(GetValidationCodeStatements());
        }

        private static string GetIdentityEntityEqualsProperties(IDataObjectInfo instance)
            => string.Join(" &&"
                + Environment.NewLine
                + "       ", instance.Fields.Where(f => f.IsIdentityField && !f.SkipFieldOnFind()).Select(f => $"{f.Name.Sanitize()} == other.{f.Name.Sanitize()}"));

        private static IEnumerable<string> GetValidationCodeStatements()
        {
            yield return "System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);";
        }

        private static IEnumerable<ClassPropertyBuilder> GetEntityIdentityClassProperties
        (
            IDataObjectInfo instance,
            RenderMetadataAsAttributesType renderMetadataAsAttributes,
            EntityClassType entityClassType
        )
        {
            return instance.Fields.Where(f => f.IsIdentityField && !f.SkipFieldOnFind())
                .Select(field => new ClassPropertyBuilder()
                    .WithName(field.Name)
                    .Fill(field)
                    .WithHasSetter(entityClassType.HasPropertySetter())
                    .AddGetterCodeStatements(GetGetterCodeStatements(field, entityClassType, false))
                    .AddSetterCodeStatements(GetSetterCodeStatements(field, entityClassType, false))
                    .AddAttributes(GetEntityClassPropertyAttributes(field, instance.Name, entityClassType, renderMetadataAsAttributes, false, false)));
        }
    }
}
