using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DataFramework.Abstractions;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.CodeStatements;
using ModelFramework.Objects.Contracts;
using ModelFramework.Objects.Default;
using ModelFramework.Objects.Extensions;
using MFAttribute = ModelFramework.Objects.Default.Attribute;
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
            var entityClassType = instance.Metadata.GetMetadataValue<EntityClassType?>(Entities.EntityClassType, null) ?? defaultEntityClassType;
            var renderMetadataAsAttributes = instance.Metadata.GetMetadataValue<RenderMetadataAsAttributesType?>(Entities.RenderMetadataAsAttributesType, null) ?? defaultRenderMetadataAsAttributes;

            return new ClassBuilder()
                .WithName(instance.Name)
                .WithNamespace(instance.Metadata.GetMetadataStringValue(Entities.Namespace, instance.TypeName?.GetNamespaceWithDefault(string.Empty) ?? string.Empty))
                .WithVisibility(instance.Metadata.GetMetadataValue(Entities.Visibility, instance.IsVisible.ToVisibility()))
                .WithBaseClass(instance.Metadata.GetMetadataStringValue(Entities.BaseClass))
                .WithPartial(true)
                .WithRecord(entityClassType == EntityClassType.Record)
                .AddInterfaces(instance.Metadata
                    .Where(md => md.Name == Entities.Interfaces)
                    .Select(md => md.Value.ToStringWithNullCheck().FixGenericParameter(instance.Name))
                    .Union(GetEntityClassTypeInterfaces(instance, entityClassType)))
                .AddFields(GetEntityClassBackingFields(instance, entityClassType))
                .AddProperties(GetEntityClassProperties(instance, renderMetadataAsAttributes, entityClassType))
                .AddMethods(GetEntityClassMethods(instance, entityClassType))
                .AddConstructors(GetEntityClassConstructors(instance, entityClassType, renderMetadataAsAttributes))
                .AddMetadata(instance.Metadata.Convert())
                .AddAttributes(GetEntityClassAttributes(instance, renderMetadataAsAttributes));
        }

        private static IEnumerable<string> GetEntityClassTypeInterfaces(IDataObjectInfo instance, EntityClassType entityClassType)
        {
            if (entityClassType == EntityClassType.ObservablePoco)
            {
                yield return typeof(INotifyPropertyChanged).FullName;
            }

            if (entityClassType == EntityClassType.ImmutablePoco)
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
                                                                                  RenderMetadataAsAttributesType renderMetadataAsAttributes,
                                                                                  EntityClassType entityClassType)
        {
            var result = new List<ClassPropertyBuilder>();
            var hasSetter = entityClassType.In(EntityClassType.Poco, EntityClassType.ObservablePoco);

            //Add properties for all instance.Fields
            foreach (var field in instance.Fields)
            {
                var prop = new ClassPropertyBuilder()
                    .WithName(field.CreatePropertyName(instance))
                    .WithTypeName(field.Metadata.GetMetadataStringValue(Entities.PropertyType, field.TypeName ?? string.Empty))
                    .WithStatic(field.Metadata.GetMetadataStringValue(Entities.Static).IsTrue())
                    .WithVirtual(field.Metadata.GetMetadataStringValue(Entities.Virtual).IsTrue())
                    .WithAbstract(field.Metadata.GetMetadataStringValue(Entities.Abstract).IsTrue())
                    .WithProtected(field.Metadata.GetMetadataStringValue(Entities.Protected).IsTrue())
                    .WithOverride(field.Metadata.GetMetadataStringValue(Entities.Override).IsTrue())
                    .WithIsNullable(field.IsNullable)
                    .WithHasGetter(true)
                    .WithHasSetter(hasSetter) //note that automatic properties need both a getter and setter. if we don't do this, the class won't compile :(
                    .WithVisibility(field.Metadata.GetMetadataValue(Entities.Visibility, field.IsVisible.ToVisibility()))
                    .WithGetterVisibility(field.Metadata.GetMetadataValue(global::ModelFramework.Objects.MetadataNames.PropertyGetterVisibility, field.IsVisible.ToVisibility()))
                    .WithSetterVisibility(field.Metadata.GetMetadataValue(global::ModelFramework.Objects.MetadataNames.PropertySetterVisibility, field.IsVisible.ToVisibility()))
                    .AddMetadata(
                        field.Metadata
                            .Convert()
                            .Concat(new[]
                                {
                                    new global::ModelFramework.Common.Builders.MetadataBuilder()
                                        .WithName(MFCommon.CustomTemplateName)
                                        .WithValue(field.Metadata.GetMetadataStringValue(MFCommon.CustomTemplateName, "CSharpClassGenerator.DefaultPropertyTemplate"))
                                        .Build()
                                }))
                    .AddAttributes(GetEntityClassPropertyAttributes(field, field.Name, instance.Name, renderMetadataAsAttributes, false))
                    .AddGetterCodeStatements(GetGetterCodeStatements(field, entityClassType))
                    .AddSetterCodeStatements(GetSetterCodeStatements(field, entityClassType));

                result.Add(prop);
            }

            foreach (var field in instance.GetUpdateConcurrencyCheckFields())
            {
                var prop = new ClassPropertyBuilder()
                    .WithName($"{field.Name}Original")
                    .WithTypeName(field.Metadata.GetMetadataStringValue(Entities.PropertyType, field.TypeName ?? string.Empty))
                    .WithStatic(field.Metadata.GetMetadataStringValue(Entities.Static).IsTrue())
                    .WithVirtual(field.Metadata.GetMetadataStringValue(Entities.Virtual).IsTrue())
                    .WithAbstract(field.Metadata.GetMetadataStringValue(Entities.Abstract).IsTrue())
                    .WithProtected(field.Metadata.GetMetadataStringValue(Entities.Protected).IsTrue())
                    .WithOverride(field.Metadata.GetMetadataStringValue(Entities.Override).IsTrue())
                    .WithIsNullable(field.IsNullable)
                    .WithHasGetter(true)
                    .WithHasSetter(hasSetter)
                    .WithVisibility(field.Metadata.GetMetadataValue(Entities.Visibility, field.IsVisible.ToVisibility()))
                    .WithGetterVisibility(field.Metadata.GetMetadataValue(global::ModelFramework.Objects.MetadataNames.PropertyGetterVisibility, field.IsVisible.ToVisibility()))
                    .WithSetterVisibility(field.Metadata.GetMetadataValue(global::ModelFramework.Objects.MetadataNames.PropertySetterVisibility, field.IsVisible.ToVisibility()))
                    .AddAttributes(new[] { new MFAttribute("System.ComponentModel.ReadOnly", new[] { new AttributeParameter(true) }) })
                    .AddGetterCodeStatements(GetGetterCodeStatementsForOriginal(field, entityClassType))
                    .AddSetterCodeStatements(GetSetterCodeStatementsForOriginal(field, entityClassType));

                result.Add(prop);
            }
            return result;
        }

        private static IEnumerable<ICodeStatement> GetSetterCodeStatementsForOriginal(IFieldInfo field, EntityClassType entityClassType)
            => GetCodeStatements(field, entityClassType, Entities.PropertySetterCodeStatement, $"_{field.Name.ToPascalCase()}Original = value;" + Environment.NewLine + string.Format(@"if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""{0}""));", field.Name));

        private static IEnumerable<ICodeStatement> GetGetterCodeStatementsForOriginal(IFieldInfo field, EntityClassType entityClassType)
            => GetCodeStatements(field, entityClassType, Entities.PropertyGetterCodeStatement, $"return _{field.Name.ToPascalCase()}Original;");

        private static IEnumerable<ICodeStatement> GetSetterCodeStatements(IFieldInfo field, EntityClassType entityClassType)
            => GetCodeStatements(field, entityClassType, Entities.PropertyGetterCodeStatement, $"_{field.Name.ToPascalCase()} = value;" + Environment.NewLine + string.Format(@"if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""{0}""));", field.Name));

        private static IEnumerable<ICodeStatement> GetGetterCodeStatements(IFieldInfo field, EntityClassType entityClassType)
        {
            var statements = field.Metadata.GetMetadataStringValues(Entities.PropertyGetterCodeStatement).ToList();
            if (!statements.Any())
            {
                statements.AddRange(field.Metadata.GetMetadataStringValues(Entities.ComputedTemplate));
            }

            if (!statements.Any() && entityClassType == EntityClassType.ObservablePoco)
            {
                statements.Add($"return _{field.Name.ToPascalCase()};");
            }
            return statements.ToLiteralCodeStatements();
        }

        private static IEnumerable<ICodeStatement> GetCodeStatements(IFieldInfo field, EntityClassType entityClassType, string metadataName, string defaultForObservable)
        {
            var statements = field.Metadata.GetMetadataStringValues(metadataName).ToList();
            if (!statements.Any() && entityClassType == EntityClassType.ObservablePoco && defaultForObservable != string.Empty)
            {
                statements.Add(defaultForObservable);
            }
            return statements.ToLiteralCodeStatements();
        }

        private static IEnumerable<AttributeBuilder> GetEntityClassPropertyAttributes(IFieldInfo field,
                                                                                      string fieldName,
                                                                                      string instanceName,
                                                                                      RenderMetadataAsAttributesType renderMetadataAsAttributes,
                                                                                      bool forBuilder,
                                                                                      bool addReadOnlyAttribute = false)
        {
            var result = new List<AttributeBuilder>();

            if (renderMetadataAsAttributes == RenderMetadataAsAttributesType.None)
            {
                return result;
            }

            result.AddModelClassAttribute("System.ComponentModel.DefaultValue", field.DefaultValue);
            result.AddModelClassAttribute("System.ComponentModel.Description", field.Description);
            result.AddModelClassAttribute("System.ComponentModel.DisplayName", field.DisplayName);
            result.AddConditionalModelClassAttribute("System.ComponentModel.ReadOnly", true, (field.IsReadOnly && !forBuilder) || addReadOnlyAttribute);
            if (!string.IsNullOrEmpty(field.DisplayName) && fieldName == instanceName && !forBuilder)
            {
                //if the field name is equal to the DataObjectInstance name, then the property will be renamed to keep the C# compiler happy.
                //in this case, we would like to add a DisplayName attribute, so the property looks right in the UI. (PropertyGrid etc.)
                result.AddModelClassAttribute("System.ComponentModel.DataAnnotations.DisplayName", fieldName);
            }

            result.AddRange(field.Metadata.GetMetadataValues<IAttribute>(Entities.EntitiesAttribute).Select(x => new AttributeBuilder(x)));

            return result;
        }

        private static IEnumerable<IClassMethod> GetEntityClassMethods(IDataObjectInfo instance, EntityClassType entityClassType)
        {
            if (entityClassType == EntityClassType.ImmutablePoco)
            {
                yield return new ClassMethod("Equals", typeof(bool).FullName, @override: true, parameters: new[] { new Parameter("obj", typeof(object).FullName) }, codeStatements: new[] { new LiteralCodeStatement($"return Equals(obj as {instance.Name});") });
                yield return new ClassMethod("Equals", typeof(bool).FullName, parameters: new[] { new Parameter("other", instance.Name) }, codeStatements: new[] { new LiteralCodeStatement($"return other != null &&{Environment.NewLine}       {GetEntityEqualsProperties(instance)};") });
                yield return new ClassMethod("GetHashCode", typeof(int).FullName, @override: true,
                    codeStatements: new[] { "int hashCode = 235838129;" }
                       .Concat(instance.Fields.Select(f => Type.GetType(f.TypeName.FixTypeName())?.IsValueType == true
                            ? $"hashCode = hashCode * -1521134295 + {f.CreatePropertyName(instance)}.GetHashCode();"
                            : $"hashCode = hashCode * -1521134295 + EqualityComparer<{f.TypeName.FixTypeName()}>.Default.GetHashCode({f.CreatePropertyName(instance)});"))
                        .Concat(new[] { "return hashCode;" })
                        .Select(x => new LiteralCodeStatement(x))
                );
                yield return new ClassMethod("==", typeof(bool).FullName, @static: true, @operator: true, parameters: new[] { new Parameter("left", instance.Name), new Parameter("right", instance.Name) }, codeStatements: new[] { new LiteralCodeStatement($"return EqualityComparer<{instance.Name}>.Default.Equals(left, right);") });
                yield return new ClassMethod("!=", typeof(bool).FullName, @static: true, @operator: true, parameters: new[] { new Parameter("left", instance.Name), new Parameter("right", instance.Name) }, codeStatements: new[] { new LiteralCodeStatement("return !(left == right);") });
            }
        }

        private static string GetEntityEqualsProperties(IDataObjectInfo instance)
            => string.Join(" &&" + Environment.NewLine + "       ", instance.Fields.Select(f => $"{f.CreatePropertyName(instance)} == other.{f.CreatePropertyName(instance)}"));

        private static IEnumerable<IClassConstructor> GetEntityClassConstructors(IDataObjectInfo instance,
                                                                                 EntityClassType entityClassType,
                                                                                 RenderMetadataAsAttributesType renderMetadataAsAttributes)
        {
            if (entityClassType.In(EntityClassType.ImmutablePoco, EntityClassType.Record))
            {
                yield return new ClassConstructor
                (
                    codeStatements: GetPocoEntityClassConstructorCodeStatements(instance, entityClassType, renderMetadataAsAttributes, true),
                    parameters: GetFieldsWithConcurrencyCheckFields(instance).Select(f => new Parameter(f.Name.ToPascalCase(), f.TypeName, f.DefaultValue, f.IsNullable))
                );
            }
        }

        private static IEnumerable<ICodeStatement> GetPocoEntityClassConstructorCodeStatements(IDataObjectInfo instance,
                                                                                               EntityClassType entityClassType,
                                                                                               RenderMetadataAsAttributesType renderMetadataAsAttributes,
                                                                                               bool createPropertyName)
        {
            if (entityClassType.In(EntityClassType.ImmutablePoco, EntityClassType.Record))
            {
                foreach (var field in GetFieldsWithConcurrencyCheckFields(instance))
                {
                    var name = createPropertyName
                        ? field.CreatePropertyName(instance)
                        : field.Name;
                    yield return new LiteralCodeStatement($"this.{name} = {field.Name.ToPascalCase()};");
                }

                if (renderMetadataAsAttributes == RenderMetadataAsAttributesType.Validation)
                {
                    // Add validation code
                    yield return new LiteralCodeStatement("System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);");
                }
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

                yield return new FieldInfoBuilder(field).WithName($"{field.Name}Original").Build();
            }
        }

        private static IEnumerable<AttributeBuilder> GetEntityClassAttributes(IDataObjectInfo instance,
                                                                              RenderMetadataAsAttributesType renderMetadataAsAttributes,
                                                                              string generatorName = "DataFramework.ModelFramework.Generators.Entities.EntityGenerator")
        {
            var result = new List<AttributeBuilder>
            {
                new AttributeBuilder(new MFAttribute
                (
                    "System.CodeDom.Compiler.GeneratedCode",
                    new[]
                    {
                        new AttributeParameter(generatorName),
                        new AttributeParameter("1.0.0.0")
                    }
                ))
            };

            if (renderMetadataAsAttributes != RenderMetadataAsAttributesType.None)
            {
                result.AddConditionalModelClassAttribute("System.ComponentModel.ReadOnly", true, instance.IsReadOnly);
                result.AddModelClassAttribute("System.ComponentModel.Description", instance.Description);
                result.AddModelClassAttribute("System.ComponentModel.DisplayName", instance.DisplayName);
                result.AddConditionalModelClassAttribute("System.ComponentModel.Browsable", false, !instance.IsVisible);
            }

            return result;
        }
    }
}
