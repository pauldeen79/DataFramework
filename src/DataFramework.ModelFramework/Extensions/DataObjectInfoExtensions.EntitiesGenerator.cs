namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoExtensions
{
    public static IClass ToEntityClass(this IDataObjectInfo instance, GeneratorSettings settings)
        => instance.ToEntityClassBuilder(settings).Build();

    public static ClassBuilder ToEntityClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
    {
        var entityClassType = instance.GetEntityClassType(settings.DefaultEntityClassType);
        var renderMetadataAsAttributes = instance.GetRenderMetadataAsAttributesType(settings.DefaultRenderMetadataAsAttributes);

        return new ClassBuilder()
            .WithName(instance.Name)
            .WithNamespace(instance.GetEntitiesNamespace())
            .FillFrom(instance)
            .WithVisibility(instance.Metadata.GetValue(Entities.Visibility, () => instance.IsVisible.ToVisibility()))
            .WithRecord(entityClassType == EntityClassType.Record)
            .AddInterfaces(instance.Metadata
                .Where(md => md.Name == Entities.Interfaces)
                .Select(md => md.Value.ToStringWithNullCheck().FixGenericParameter(instance.Name))
                .Union(GetEntityClassInterfaces(instance, entityClassType)))
            .AddFields(GetEntityClassFields(instance, entityClassType))
            .AddProperties(GetEntityClassProperties(instance, entityClassType, renderMetadataAsAttributes))
            .AddMethods(GetEntityClassMethods(instance, entityClassType))
            .AddConstructors(GetEntityClassConstructors(instance, entityClassType, settings))
            .AddAttributes(GetEntityClassAttributes(instance, renderMetadataAsAttributes));
    }

    private static IEnumerable<string> GetEntityClassInterfaces(IDataObjectInfo instance,
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

    private static IEnumerable<ClassFieldBuilder> GetEntityClassFields(IDataObjectInfo instance,
                                                                       EntityClassType entityClassType)
    {
        if (entityClassType != EntityClassType.ObservablePoco)
        {
            yield break;
        }

        foreach (var field in instance.Fields)
        {
            yield return new ClassFieldBuilder().FillFrom(field);
        }

        foreach (var field in instance.GetUpdateConcurrencyCheckFields())
        {
            yield return new ClassFieldBuilder()
                .FillFrom(field)
                .WithName($"_{field.Name.ToPascalCase()}Original")
                .WithIsNullable(true);
        }

        yield return new ClassFieldBuilder()
            .WithName(nameof(INotifyPropertyChanged.PropertyChanged))
            .WithType(typeof(PropertyChangedEventHandler))
            .WithEvent()
            .WithVisibility(Visibility.Public);
    }

    private static IEnumerable<ClassPropertyBuilder> GetEntityClassProperties(IDataObjectInfo instance,
                                                                              EntityClassType entityClassType,
                                                                              RenderMetadataAsAttributesTypes renderMetadataAsAttributes)
        => instance.Fields.Select(field =>
                new ClassPropertyBuilder()
                    .WithName(field.CreatePropertyName(instance))
                    .Fill(field)
                    .WithHasSetter(!field.IsComputed && field.CanSet && entityClassType.HasPropertySetter())
                    .AddAttributes(GetEntityClassPropertyAttributes(field, instance.Name, entityClassType, renderMetadataAsAttributes, false))
                    .AddGetterCodeStatements(GetGetterCodeStatements(field, entityClassType))
                    .AddSetterLiteralCodeStatements(GetSetterCodeStatements(field, entityClassType).ToArray()))
            .Concat(instance.GetUpdateConcurrencyCheckFields().Select(field =>
                new ClassPropertyBuilder()
                    .WithName($"{field.Name}Original")
                    .Fill(field)
                    .WithIsNullable()
                    .WithHasSetter(entityClassType.HasPropertySetter())
                    .AddAttributes(entityClassType.IsImmutable()
                        ? Enumerable.Empty<AttributeBuilder>()
                        : new[] { new AttributeBuilder().AddNameAndParameter("System.ComponentModel.ReadOnly", true) })
                    .AddGetterLiteralCodeStatements(GetGetterCodeStatementsForOriginal(field, entityClassType).ToArray())
                    .AddSetterLiteralCodeStatements(GetSetterCodeStatementsForOriginal(field, entityClassType).ToArray())));

    private static IEnumerable<string> GetSetterCodeStatementsForOriginal(IFieldInfo field,
                                                                                         EntityClassType entityClassType)
    {
        if (entityClassType == EntityClassType.ObservablePoco)
        {
            yield return $"_{field.Name.Sanitize().ToPascalCase()}Original = value;";
            yield return $@"if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""{field.Name.Sanitize()}""));";
        }
    }

    private static IEnumerable<string> GetGetterCodeStatementsForOriginal(IFieldInfo field, EntityClassType entityClassType)
    {
        if (entityClassType == EntityClassType.ObservablePoco)
        {
            yield return $"return _{field.Name.Sanitize().ToPascalCase()}Original;";
        }
    }

    private static IEnumerable<string> GetSetterCodeStatements(IFieldInfo field, EntityClassType entityClassType)
    {
        if (entityClassType == EntityClassType.ObservablePoco)
        {
            yield return $"_{field.Name.Sanitize().ToPascalCase()} = value;";
            yield return string.Format(@"if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(""{0}""));", field.Name.Sanitize());
        }
    }

    private static IEnumerable<ICodeStatementBuilder> GetGetterCodeStatements(IFieldInfo field, EntityClassType entityClassType)
    {
        var statements = field.Metadata.GetValues<ICodeStatement>(Entities.ComputedFieldStatement).Select(x => x.CreateBuilder()).ToList();

        if (!statements.Any() && entityClassType == EntityClassType.ObservablePoco)
        {
            statements.Add(new LiteralCodeStatementBuilder().WithStatement($"return _{field.Name.Sanitize().ToPascalCase()};"));
        }
        return statements;
    }

    private static IEnumerable<AttributeBuilder> GetEntityClassPropertyAttributes(IFieldInfo field,
                                                                                  string instanceName,
                                                                                  EntityClassType entityClassType,
                                                                                  RenderMetadataAsAttributesTypes renderMetadataAsAttributes,
                                                                                  bool addReadOnlyAttribute)
    {
        if (string.IsNullOrEmpty(field.DisplayName) && field.Name == instanceName)
        {
            //if the field name is equal to the DataObjectInstance name, then the property will be renamed to keep the C# compiler happy.
            //in this case, we would like to add a DisplayName attribute, so the property looks right in the UI. (PropertyGrid etc.)
            yield return new AttributeBuilder().AddNameAndParameter("System.ComponentModel.DataAnnotations.DisplayName", field.Name);
        }

        if (renderMetadataAsAttributes.HasFlag(RenderMetadataAsAttributesTypes.Validation))
        {
            foreach (var attribute in GetEntityClassPropertyValidationAttributes(field, entityClassType, addReadOnlyAttribute))
            {
                yield return attribute;
            }
        }

        if (renderMetadataAsAttributes.HasFlag(RenderMetadataAsAttributesTypes.Custom))
        {
            foreach (var attribute in field.Metadata.GetValues<IAttribute>(Entities.FieldAttribute))
            {
                yield return new AttributeBuilder(attribute);
            }
        }
    }

    private static IEnumerable<AttributeBuilder> GetEntityClassPropertyValidationAttributes(IFieldInfo field,
                                                                                            EntityClassType entityClassType,
                                                                                            bool addReadOnlyAttribute)
    {
        if (field.DefaultValue != null)
        {
            yield return new AttributeBuilder().AddNameAndParameter("System.ComponentModel.DefaultValue", field.DefaultValue);
        }

        if (!string.IsNullOrEmpty(field.Description))
        {
            yield return new AttributeBuilder().AddNameAndOptionalParameter("System.ComponentModel.Description", field.Description);
        }

        if (!string.IsNullOrEmpty(field.DisplayName))
        {
            yield return new AttributeBuilder().AddNameAndOptionalParameter("System.ComponentModel.DisplayName", field.DisplayName);
        }

        if ((field.IsReadOnly && !entityClassType.IsImmutable()) || addReadOnlyAttribute)
        {
            yield return new AttributeBuilder().AddNameAndParameter("System.ComponentModel.ReadOnly", true);
        }
    }

    private static IEnumerable<ClassMethodBuilder> GetEntityClassMethods(IDataObjectInfo instance, EntityClassType entityClassType)
        => ClassMethods(instance,
                        instance.Name,
                        GetEntityEqualsProperties(instance),
                        instance.Fields,
                        entityClassType);

    private static IEnumerable<ClassMethodBuilder> ClassMethods(IDataObjectInfo instance,
                                                                string instanceName,
                                                                string equalsProperties,
                                                                IEnumerable<IFieldInfo> fields,
                                                                EntityClassType entityClassType)
    {
        if (entityClassType != EntityClassType.ImmutableClass)
        {
            yield break;
        }

        yield return new ClassMethodBuilder().WithName(nameof(object.Equals))
                                             .WithType(typeof(bool))
                                             .WithOverride()
                                             .AddParameter("obj", typeof(object))
                                             .AddLiteralCodeStatements($"return Equals(obj as {instanceName});");

        yield return new ClassMethodBuilder().WithName(nameof(IEquatable<object>.Equals))
                                             .WithType(typeof(bool))
                                             .AddParameter("other", instanceName)
                                             .AddLiteralCodeStatements($"return other != null &&{Environment.NewLine}       {equalsProperties};");

        yield return new ClassMethodBuilder().WithName(nameof(object.GetHashCode))
                                             .WithType(typeof(int))
                                             .WithOverride()
                                             .AddLiteralCodeStatements("int hashCode = 235838129;")
                                             .AddLiteralCodeStatements(fields.Select(f => Type.GetType(f.GetPropertyTypeName().FixTypeName()).IsValueType
                                                ? $"hashCode = hashCode * -1521134295 + {f.CreatePropertyName(instance)}.GetHashCode();"
                                                : $"hashCode = hashCode * -1521134295 + EqualityComparer<{f.GetPropertyTypeName().FixTypeName()}>.Default.GetHashCode({f.CreatePropertyName(instance)});"))
                                             .AddLiteralCodeStatements("return hashCode;");

        yield return new ClassMethodBuilder().WithName("==")
                                             .WithType(typeof(bool))
                                             .WithStatic()
                                             .WithOperator()
                                             .AddParameter("left", instanceName)
                                             .AddParameter("right", instanceName)
                                             .AddLiteralCodeStatements($"return EqualityComparer<{instanceName}>.Default.Equals(left, right);");

        yield return new ClassMethodBuilder().WithName("!=")
                                             .WithType(typeof(bool))
                                             .WithStatic()
                                             .WithOperator()
                                             .AddParameter("left", instanceName)
                                             .AddParameter("right", instanceName)
                                             .AddLiteralCodeStatements("return !(left == right);");
    }

    private static string GetEntityEqualsProperties(IDataObjectInfo instance)
        => string.Join(" &&"
            + Environment.NewLine
            + "       ", instance.Fields.Select(f => $"{f.CreatePropertyName(instance)} == other.{f.CreatePropertyName(instance)}"));

    private static IEnumerable<ClassConstructorBuilder> GetEntityClassConstructors(IDataObjectInfo instance,
                                                                                   EntityClassType entityClassType,
                                                                                   GeneratorSettings settings)
    {
        if (entityClassType.IsImmutable())
        {
            yield return new ClassConstructorBuilder()
                .AddLiteralCodeStatements(GetEntityClassConstructorCodeStatements(instance, settings))
                .AddParameters(GetEditableFieldsWithConcurrencyCheckFields(instance).Select(f => f.ToParameterBuilder()));
        }
    }

    private static IEnumerable<string> GetEntityClassConstructorCodeStatements(IDataObjectInfo instance,
                                                                               GeneratorSettings settings)
    {
        foreach (var field in GetEditableFieldsWithConcurrencyCheckFields(instance))
        {
            yield return $"this.{field.CreatePropertyName(instance)} = {field.Name.Sanitize().ToPascalCase()};";
        }

        if (settings.AddValidationCodeInConstructor)
        {
            yield return "System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);";
        }
    }

    private static IEnumerable<IFieldInfo> GetEditableFieldsWithConcurrencyCheckFields(IDataObjectInfo instance)
    {
        foreach (var field in instance.Fields.Where(f => !f.IsComputed && f.CanSet))
        {
            yield return field;
        }

        var concurrencyCheckBehavior = instance.GetConcurrencyCheckBehavior();
        foreach (var field in instance.GetUpdateConcurrencyCheckFields())
        {
            if (!field.IsIdentityField
                && !field.IsSqlIdentity()
                && !field.UseForConcurrencyCheck
                && concurrencyCheckBehavior != ConcurrencyCheckBehavior.AllFields)
            {
                continue;
            }

            yield return new FieldInfoBuilder(field)
                .WithName($"{field.Name}Original")
                .WithIsNullable()
                .WithDefaultValue(new global::ModelFramework.Common.Literal("default"))
                .Build();
        }
    }

    private static IEnumerable<AttributeBuilder> GetEntityClassAttributes(IDataObjectInfo instance,
                                                                          RenderMetadataAsAttributesTypes renderMetadataAsAttributes)
    {
        yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.Entities.EntityGenerator");

        if (renderMetadataAsAttributes.HasFlag(RenderMetadataAsAttributesTypes.Validation))
        {
            if (instance.IsReadOnly)
            {
                yield return new AttributeBuilder().AddNameAndParameter("System.ComponentModel.ReadOnly", true);
            }

            if (!string.IsNullOrEmpty(instance.Description))
            {
                yield return new AttributeBuilder().AddNameAndOptionalParameter("System.ComponentModel.Description", instance.Description);
            }

            if (!string.IsNullOrEmpty(instance.DisplayName))
            {
                yield return new AttributeBuilder().AddNameAndOptionalParameter("System.ComponentModel.DisplayName", instance.DisplayName);
            }

            if (!instance.IsVisible)
            {
                yield return new AttributeBuilder().AddNameAndParameter("System.ComponentModel.Browsable", false);
            }
        }

        foreach (var attribute in instance.GetClassAttributeBuilderAttributes(renderMetadataAsAttributes, Entities.ClassAttribute))
        {
            yield return attribute;
        }
    }
}
