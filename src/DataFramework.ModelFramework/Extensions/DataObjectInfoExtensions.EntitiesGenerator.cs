using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DataFramework.Abstractions;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.CodeStatements.Builders;
using ModelFramework.Objects.Contracts;
using ModelFramework.Objects.Extensions;
using MFCommon = ModelFramework.Common.MetadataNames;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        public static ClassBuilder ToEntityClassBuilder
        (
            this IDataObjectInfo instance,
            RenderMetadataAsAttributesType defaultRenderMetadataAsAttributes = RenderMetadataAsAttributesType.Validation,
            EntityClassType defaultEntityClassType = EntityClassType.Poco
        )
        {
            var entityClassType = instance.GetEntityClassType(defaultEntityClassType);
            var renderMetadataAsAttributes = instance.GetRenderMetadataAsAttributesType(defaultRenderMetadataAsAttributes);

            return new ClassBuilder()
                .WithName(instance.Name)
                .WithNamespace(instance.GetEntitiesNamespace())
                .WithSharedDataObjectInfoData(instance)
                .WithVisibility(instance.Metadata.GetMetadataValue(Entities.Visibility, instance.IsVisible.ToVisibility()))
                .WithBaseClass(instance.Metadata.GetMetadataStringValue(Entities.BaseClass))
                .WithRecord(entityClassType == EntityClassType.Record)
                .AddInterfaces(instance.Metadata
                    .Where(md => md.Name == Entities.Interfaces)
                    .Select(md => md.Value.ToStringWithNullCheck().FixGenericParameter(instance.Name))
                    .Union(GetEntityClassTypeInterfaces(instance, entityClassType)))
                .AddFields(GetEntityClassBackingFields(instance, entityClassType))
                .AddProperties(GetEntityClassProperties(instance, entityClassType, renderMetadataAsAttributes))
                .AddMethods(GetEntityClassMethods(instance, entityClassType))
                .AddConstructors(GetEntityClassConstructors(instance, entityClassType, renderMetadataAsAttributes))
                .AddAttributes(GetEntityClassAttributes(instance, renderMetadataAsAttributes));
        }

        private static IEnumerable<string> GetEntityClassTypeInterfaces(IDataObjectInfo instance,
                                                                        EntityClassType entityClassType)
        {
            if (entityClassType == EntityClassType.ObservablePoco)
            {
                yield return typeof(INotifyPropertyChanged).FullName;
            }

            if (entityClassType == EntityClassType.ImmutableClass)
            {
                yield return $"IEquatable<{instance.Name}>";
            }
        }

        private static IEnumerable<ClassFieldBuilder> GetEntityClassBackingFields(IDataObjectInfo instance,
                                                                                  EntityClassType entityClassType)
        {
            if (entityClassType != EntityClassType.ObservablePoco)
            {
                yield break;
            }

            foreach (var field in instance.Fields)
            {
                yield return new ClassFieldBuilder()
                    .WithName($"_{field.Name.ToPascalCase()}")
                    .WithTypeName(field.TypeName)
                    .WithIsNullable(field.IsNullable)
                    .WithVisibility(Visibility.Private);
            }

            foreach (var field in instance.GetUpdateConcurrencyCheckFields())
            {
                yield return new ClassFieldBuilder()
                    .WithName($"_{field.Name.ToPascalCase()}Original")
                    .WithTypeName(field.TypeName)
                    .WithIsNullable(field.IsNullable)
                    .WithVisibility(Visibility.Private);
            }

            yield return new ClassFieldBuilder()
                .WithName(nameof(INotifyPropertyChanged.PropertyChanged))
                .WithTypeName(typeof(PropertyChangedEventHandler).FullName)
                .WithEvent()
                .WithVisibility(Visibility.Public);
        }

        private static IEnumerable<ClassPropertyBuilder> GetEntityClassProperties(IDataObjectInfo instance,
                                                                                  EntityClassType entityClassType,
                                                                                  RenderMetadataAsAttributesType renderMetadataAsAttributes)
            => instance.Fields.Select(field =>
                    new ClassPropertyBuilder()
                        .WithName(field.CreatePropertyName(instance))
                        .WithSharedFieldInfoData(field)
                        .WithHasSetter(entityClassType.HasPropertySetter())
                        .AddMetadata(new global::ModelFramework.Common.Builders.MetadataBuilder()
                            .WithName(MFCommon.CustomTemplateName)
                            .WithValue(field.Metadata.GetMetadataStringValue(MFCommon.CustomTemplateName, "CSharpClassGenerator.DefaultPropertyTemplate"))
                            .Build())
                        .AddAttributes(GetEntityClassPropertyAttributes(field, instance.Name, entityClassType, renderMetadataAsAttributes, false, false))
                        .AddGetterCodeStatements(GetGetterCodeStatements(field, entityClassType, false))
                        .AddSetterCodeStatements(GetSetterCodeStatements(field, entityClassType, false)))
                .Concat(instance.GetUpdateConcurrencyCheckFields().Select(field =>
                    new ClassPropertyBuilder()
                        .WithName($"{field.Name}Original")
                        .WithSharedFieldInfoData(field)
                        .WithIsNullable(true)
                        .WithHasSetter(entityClassType.HasPropertySetter())
                        .AddAttributes(entityClassType.IsImmutable()
                            ? Enumerable.Empty<AttributeBuilder>()
                            : new[] { new AttributeBuilder().WithName("System.ComponentModel.ReadOnly")
                                                            .AddParameters(new AttributeParameterBuilder().WithValue(true)) })
                        .AddGetterCodeStatements(GetGetterCodeStatementsForOriginal(field, entityClassType))
                        .AddSetterCodeStatements(GetSetterCodeStatementsForOriginal(field, entityClassType))));

        private static IEnumerable<ICodeStatementBuilder> GetSetterCodeStatementsForOriginal(IFieldInfo field,
                                                                                             EntityClassType entityClassType)
            => GetCodeStatements(field,
                                 entityClassType,
                                 Entities.OriginalPropertySetterCodeStatement,
                                 $"_{field.Name.ToPascalCase()}Original = value;"
                                    + Environment.NewLine
                                    + string.Format(@"if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""{0}""));", field.Name));

        private static IEnumerable<ICodeStatementBuilder> GetGetterCodeStatementsForOriginal(IFieldInfo field,
                                                                                             EntityClassType entityClassType)
            => GetCodeStatements(field,
                                 entityClassType,
                                 Entities.OriginalPropertyGetterCodeStatement,
                                 $"return _{field.Name.ToPascalCase()}Original;");

        private static IEnumerable<ICodeStatementBuilder> GetSetterCodeStatements(IFieldInfo field,
                                                                                  EntityClassType entityClassType,
                                                                                  bool forBuilder)
            => GetCodeStatements(field, entityClassType, Entities.PropertySetterCodeStatement, forBuilder
                ? $"_{field.Name.ToPascalCase()} = value;"
                    + Environment.NewLine
                    + string.Format(@"if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""{0}""));", field.Name)
                : string.Empty);

        private static IEnumerable<ICodeStatementBuilder> GetGetterCodeStatements(IFieldInfo field,
                                                                                  EntityClassType entityClassType,
                                                                                  bool forBuilder)
        {
            var statements = field.Metadata.GetMetadataStringValues(Entities.PropertyGetterCodeStatement).ToList();
            if (!statements.Any())
            {
                statements.AddRange(field.Metadata.GetMetadataStringValues(Entities.ComputedTemplate));
            }

            if (!statements.Any() && entityClassType == EntityClassType.ObservablePoco && !forBuilder)
            {
                statements.Add($"return _{field.Name.ToPascalCase()};");
            }
            return statements.ToLiteralCodeStatementBuilders();
        }

        private static IEnumerable<ICodeStatementBuilder> GetCodeStatements(IFieldInfo field,
                                                                            EntityClassType entityClassType,
                                                                            string metadataName,
                                                                            string defaultForObservable)
        {
            var statements = field.Metadata.GetMetadataStringValues(metadataName).ToList();
            if (!statements.Any() && entityClassType == EntityClassType.ObservablePoco && defaultForObservable != string.Empty)
            {
                statements.Add(defaultForObservable);
            }
            return statements.ToLiteralCodeStatementBuilders();
        }

        private static IEnumerable<AttributeBuilder> GetEntityClassPropertyAttributes(IFieldInfo field,
                                                                                      string instanceName,
                                                                                      EntityClassType entityClassType,
                                                                                      RenderMetadataAsAttributesType renderMetadataAsAttributes,
                                                                                      bool forBuilder,
                                                                                      bool addReadOnlyAttribute)
        {
            if (renderMetadataAsAttributes == RenderMetadataAsAttributesType.None)
            {
                yield break;
            }

            if (field.DefaultValue != null)
            {
                yield return new AttributeBuilder().WithName("System.ComponentModel.DefaultValue")
                                                   .AddParameters(new AttributeParameterBuilder().WithValue(field.DefaultValue));
            }

            if (!string.IsNullOrEmpty(field.Description))
            {
                yield return new AttributeBuilder().WithName("System.ComponentModel.Description")
                                                   .AddParameters(new AttributeParameterBuilder().WithValue(field.Description));
            }

            if (!string.IsNullOrEmpty(field.DisplayName))
            {
                yield return new AttributeBuilder().WithName("System.ComponentModel.DisplayName")
                                                   .AddParameters(new AttributeParameterBuilder().WithValue(field.DisplayName));
            }

            if ((field.IsReadOnly
                && !forBuilder
                && entityClassType != EntityClassType.Record
                && entityClassType != EntityClassType.ImmutableClass) || addReadOnlyAttribute)
            {
                yield return new AttributeBuilder().WithName("System.ComponentModel.ReadOnly")
                                                   .AddParameters(new AttributeParameterBuilder().WithValue(true));
            }

            if (!string.IsNullOrEmpty(field.DisplayName) && field.Name == instanceName && !forBuilder)
            {
                //if the field name is equal to the DataObjectInstance name, then the property will be renamed to keep the C# compiler happy.
                //in this case, we would like to add a DisplayName attribute, so the property looks right in the UI. (PropertyGrid etc.)
                yield return new AttributeBuilder().WithName("System.ComponentModel.DataAnnotations.DisplayName")
                                                   .AddParameters(new AttributeParameterBuilder().WithValue(field.Name));
            }

            foreach (var attribute in field.Metadata.GetMetadataValues<IAttribute>(Entities.EntitiesAttribute))
            {
                yield return new AttributeBuilder(attribute);
            }
        }

        private static IEnumerable<ClassMethodBuilder> GetEntityClassMethods(IDataObjectInfo instance, EntityClassType entityClassType)
        {
            if (entityClassType == EntityClassType.ImmutableClass)
            {
                yield return new ClassMethodBuilder().WithName("Equals")
                                                     .WithType(typeof(bool))
                                                     .WithOverride()
                                                     .AddParameters(new ParameterBuilder().WithName("obj")
                                                                                          .WithType(typeof(object)))
                                                     .AddCodeStatements(new LiteralCodeStatementBuilder().WithStatement($"return Equals(obj as {instance.Name});"));

                yield return new ClassMethodBuilder().WithName("Equals")
                                                     .WithType(typeof(bool))
                                                     .AddParameters(new ParameterBuilder().WithName("other")
                                                                                          .WithTypeName(instance.Name))
                                                     .AddCodeStatements(new LiteralCodeStatementBuilder().WithStatement($"return other != null &&{Environment.NewLine}       {GetEntityEqualsProperties(instance)};"));

                yield return new ClassMethodBuilder().WithName("GetHashCode")
                                                     .WithType(typeof(int))
                                                     .WithOverride()
                                                     .AddCodeStatements(new LiteralCodeStatementBuilder().WithStatement("int hashCode = 235838129;"))
                                                     .AddCodeStatements(instance.Fields.Select(f => Type.GetType(f.TypeName.FixTypeName())?.IsValueType == true
                                                        ? new LiteralCodeStatementBuilder().WithStatement($"hashCode = hashCode * -1521134295 + {f.CreatePropertyName(instance)}.GetHashCode();")
                                                        : new LiteralCodeStatementBuilder().WithStatement($"hashCode = hashCode * -1521134295 + EqualityComparer<{f.TypeName.FixTypeName()}>.Default.GetHashCode({f.CreatePropertyName(instance)});")))
                                                     .AddCodeStatements(new LiteralCodeStatementBuilder().WithStatement("return hashCode;"));
                        
                yield return new ClassMethodBuilder().WithName("==")
                                                     .WithType(typeof(bool))
                                                     .WithStatic()
                                                     .WithOperator()
                                                     .AddParameters(new ParameterBuilder().WithName("left")
                                                                                          .WithTypeName(instance.Name),
                                                                    new ParameterBuilder().WithName("right")
                                                                                          .WithTypeName(instance.Name))
                                                     .AddCodeStatements(new LiteralCodeStatementBuilder().WithStatement($"return EqualityComparer<{instance.Name}>.Default.Equals(left, right);"));

                yield return new ClassMethodBuilder().WithName("!=")
                                                     .WithType(typeof(bool))
                                                     .WithStatic()
                                                     .WithOperator()
                                                     .AddParameters(new ParameterBuilder().WithName("left")
                                                                                          .WithTypeName(instance.Name),
                                                                    new ParameterBuilder().WithName("right")
                                                                                          .WithTypeName(instance.Name))
                                                     .AddCodeStatements(new LiteralCodeStatementBuilder().WithStatement("return !(left == right);"));
            }
        }

        private static string GetEntityEqualsProperties(IDataObjectInfo instance)
            => string.Join(" &&" + Environment.NewLine + "       ", instance.Fields.Select(f => $"{f.CreatePropertyName(instance)} == other.{f.CreatePropertyName(instance)}"));

        private static IEnumerable<ClassConstructorBuilder> GetEntityClassConstructors(IDataObjectInfo instance,
                                                                                       EntityClassType entityClassType,
                                                                                       RenderMetadataAsAttributesType renderMetadataAsAttributes)
        {
            if (entityClassType.IsImmutable())
            {
                yield return new ClassConstructorBuilder()
                    .AddCodeStatements(GetEntityClassConstructorCodeStatements(instance, entityClassType, renderMetadataAsAttributes, true))
                    .AddParameters(GetFieldsWithConcurrencyCheckFields(instance).Select(f => f.ToParameterBuilder()));
            }
        }

        private static IEnumerable<ICodeStatementBuilder> GetEntityClassConstructorCodeStatements(IDataObjectInfo instance,
                                                                                                  EntityClassType entityClassType,
                                                                                                  RenderMetadataAsAttributesType renderMetadataAsAttributes,
                                                                                                  bool createPropertyName)
        {
            if (!entityClassType.IsImmutable())
            {
                yield break;
            }
            
            foreach (var field in GetFieldsWithConcurrencyCheckFields(instance))
            {
                var name = createPropertyName
                    ? field.CreatePropertyName(instance)
                    : field.Name;
                yield return new LiteralCodeStatementBuilder().WithStatement($"this.{name} = {field.Name.ToPascalCase()};");
            }

            if (renderMetadataAsAttributes == RenderMetadataAsAttributesType.Validation)
            {
                // Add validation code
                yield return new LiteralCodeStatementBuilder()
                    .WithStatement("System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);");
            }
        }

        private static IEnumerable<IFieldInfo> GetFieldsWithConcurrencyCheckFields(IDataObjectInfo instance)
        {
            foreach (var field in instance.Fields)
            {
                yield return field;
            }

            var concurrencyCheckBehavior = instance.GetConcurrencyCheckBehavior();
            foreach (var field in instance.GetUpdateConcurrencyCheckFields())
            {
                if (!field.IsIdentityField
                    && !field.UseForCheckOnOriginalValues
                    && concurrencyCheckBehavior != ConcurrencyCheckBehavior.AllFields)
                {
                    continue;
                }

                yield return new FieldInfoBuilder(field)
                    .WithName($"{field.Name}Original")
                    .WithIsNullable()
                    .WithDefaultValue(new Literal("default"))
                    .Build();
            }
        }

        private static IEnumerable<AttributeBuilder> GetEntityClassAttributes(IDataObjectInfo instance,
                                                                              RenderMetadataAsAttributesType renderMetadataAsAttributes,
                                                                              string generatorName = "DataFramework.ModelFramework.Generators.Entities.EntityGenerator")
        {
            yield return new AttributeBuilder().ForCodeGenerator(generatorName);

            if (renderMetadataAsAttributes != RenderMetadataAsAttributesType.None)
            {
                if (instance.IsReadOnly)
                {
                    yield return new AttributeBuilder().WithName("System.ComponentModel.ReadOnly")
                                                       .AddParameters(new AttributeParameterBuilder().WithValue(true));
                }

                if (!string.IsNullOrEmpty(instance.Description))
                {
                    yield return new AttributeBuilder().WithName("System.ComponentModel.Description")
                                                       .AddParameters(new AttributeParameterBuilder().WithValue(instance.Description));
                }

                if (!string.IsNullOrEmpty(instance.DisplayName))
                {
                    yield return new AttributeBuilder().WithName("System.ComponentModel.DisplayName")
                                                       .AddParameters(new AttributeParameterBuilder().WithValue(instance.DisplayName));
                }

                if (!instance.IsVisible)
                {
                    yield return new AttributeBuilder().WithName("System.ComponentModel.Browsable")
                                                       .AddParameters(new AttributeParameterBuilder().WithValue(false));
                }
            }
        }
    }
}
