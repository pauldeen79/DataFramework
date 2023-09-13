namespace DataFramework.ModelFramework.Extensions;

public static partial class DataObjectInfoExtensions
{
    public static IClass ToCommandEntityProviderClass(this IDataObjectInfo instance, GeneratorSettings settings)
        => instance.ToCommandEntityProviderClassBuilder(settings).BuildTyped();

    public static ClassBuilder ToCommandEntityProviderClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
        => new ClassBuilder()
            .WithName($"{instance.Name}CommandEntityProvider")
            .WithNamespace(instance.GetCommandEntityProvidersNamespace())
            .FillFrom(instance)
            .WithVisibility(instance.Metadata.GetValue(CommandEntityProviders.Visibility, () => instance.IsVisible.ToVisibility()))
            .AddInterfaces(typeof(IDatabaseCommandEntityProvider<,>).CreateGenericTypeName(instance.GetEntityFullName(), instance.GetEntityBuilderFullName()))
            .AddAttributes(GetEntityCommandProviderClassAttributes(instance))
            .AddProperties(GetEntityCommandProviderClassProperties(instance))
            .AddMethods(GetEntityCommandProviderClassMethods(instance, instance.GetEntityClassType(settings.DefaultEntityClassType)));

    private static IEnumerable<AttributeBuilder> GetEntityCommandProviderClassAttributes(IDataObjectInfo instance)
    {
        yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.EntityCommandProviderGenerator");

        foreach (var attribute in instance.Metadata.GetValues<IAttribute>(CommandEntityProviders.Attribute))
        {
            yield return new AttributeBuilder(attribute);
        }
    }

    private static IEnumerable<ClassPropertyBuilder> GetEntityCommandProviderClassProperties(IDataObjectInfo instance)
    {
        yield return new ClassPropertyBuilder()
            .WithName($"{nameof(IDatabaseCommandEntityProvider<object, string>.ResultEntityDelegate)}")
            .WithTypeName($"Func<{instance.GetEntityBuilderFullName()}, {typeof(DatabaseOperation).FullName}, {instance.GetEntityBuilderFullName()}>")
            .WithIsNullable()
            .WithHasSetter(false)
            .AddGetterLiteralCodeStatements
            (
                "return (entity, operation) =>",
                "{",
                "    switch (operation)",
                "    {"
            )
            .AddEntityCommandProviderMethod(instance, CommandProviders.PreventAdd, DatabaseOperation.Insert, "ResultEntity")
            .AddEntityCommandProviderMethod(instance, CommandProviders.PreventUpdate, DatabaseOperation.Update, "ResultEntity")
            .AddEntityCommandProviderMethod(instance, CommandProviders.PreventDelete, DatabaseOperation.Delete, "ResultEntity")
            .AddGetterLiteralCodeStatements
            (
                "         default:",
                $"             throw new {nameof(ArgumentOutOfRangeException)}(\"operation\", string.Format(\"Unsupported operation: {{0}}\", operation));",
                "    }",
                "}"
            );

        yield return new ClassPropertyBuilder()
            .WithName($"{nameof(IDatabaseCommandEntityProvider<object, string>.AfterReadDelegate)}")
            .WithTypeName($"Func<{instance.GetEntityBuilderFullName()}, {typeof(DatabaseOperation).FullName}, {typeof(IDataReader).FullName}, {instance.GetEntityBuilderFullName()}>")
            .WithIsNullable()
            .WithHasSetter(false)
            .AddGetterLiteralCodeStatements
            (
                "return (entity, operation, reader) =>",
                "{",
                "    switch (operation)",
                "    {"
            )
            .AddEntityCommandProviderMethod(instance, CommandProviders.PreventAdd, DatabaseOperation.Insert, "AfterRead")
            .AddEntityCommandProviderMethod(instance, CommandProviders.PreventUpdate, DatabaseOperation.Update, "AfterRead")
            .AddEntityCommandProviderMethod(instance, CommandProviders.PreventDelete, DatabaseOperation.Delete, "AfterRead")
            .AddGetterLiteralCodeStatements
            (
                "         default:",
                $"             throw new {nameof(ArgumentOutOfRangeException)}(\"operation\", string.Format(\"Unsupported operation: {{0}}\", operation));",
                "    }",
                "}"
            );

        yield return new ClassPropertyBuilder()
            .WithName($"{nameof(IDatabaseCommandEntityProvider<object, string>.CreateBuilderDelegate)}")
            .WithTypeName($"Func<{instance.GetEntityBuilderFullName()}, {instance.GetEntityFullName()}>")
            .WithIsNullable()
            .WithHasSetter(false)
            .AddGetterLiteralCodeStatements($"return entity => new {instance.GetEntityBuilderFullName()}(entity);");

        yield return new ClassPropertyBuilder()
            .WithName($"{nameof(IDatabaseCommandEntityProvider<object, string>.CreateEntityDelegate)}")
            .WithTypeName($"Func<{instance.GetEntityBuilderFullName()}, {instance.GetEntityFullName()}>")
            .WithIsNullable()
            .WithHasSetter(false)
            .AddGetterLiteralCodeStatements("return builder => builder.Build();");
    }

    private static IEnumerable<ClassMethodBuilder> GetEntityCommandProviderClassMethods(IDataObjectInfo instance, EntityClassType entityClassType)
    {
        var outputFields = instance.GetOutputFields().ToArray();
        var originalFields = instance.GetUpdateConcurrencyCheckFields().ToArray();
        var outputFieldsForOriginal = outputFields.Where(x => originalFields.Contains(x)).ToArray();

        if (!instance.Metadata.GetBooleanValue(CommandProviders.PreventAdd))
        {
            yield return new ClassMethodBuilder()
                .WithName("AddResultEntity")
                .WithTypeName(instance.GetEntityBuilderFullName())
                .AddParameter("resultEntity", instance.GetEntityBuilderFullName())
                .AddCodeStatements(instance.Metadata.GetValues<ICodeStatement>(CommandEntityProviders.AddResultEntityStatement).Select(x => x.CreateBuilder()))
                .AddLiteralCodeStatements("return resultEntity;");

            yield return new ClassMethodBuilder()
                .WithName("AddAfterRead")
                .WithTypeName(instance.GetEntityBuilderFullName())
                .AddParameter("resultEntity", instance.GetEntityBuilderFullName())
                .AddParameter("reader", typeof(IDataReader))
                .AddLiteralCodeStatements(outputFields.Select(x => CreateAfterReadStatement(x, instance, entityClassType.IsImmutable(), string.Empty)))
                .AddLiteralCodeStatements(outputFieldsForOriginal.Select(x => CreateAfterReadStatement(x, instance, entityClassType.IsImmutable(), "Original")))
                .AddCodeStatements(instance.Metadata.GetValues<ICodeStatement>(CommandEntityProviders.AddAfterReadStatement).Select(x => x.CreateBuilder()))
                .AddLiteralCodeStatements("return resultEntity;");
        }

        if (!instance.Metadata.GetBooleanValue(CommandProviders.PreventUpdate))
        {
            yield return new ClassMethodBuilder()
                .WithName("UpdateResultEntity")
                .WithTypeName(instance.GetEntityBuilderFullName())
                .AddParameter("resultEntity", instance.GetEntityBuilderFullName())
                .AddCodeStatements(instance.Metadata.GetValues<ICodeStatement>(CommandEntityProviders.UpdateResultEntityStatement).Select(x => x.CreateBuilder()))
                .AddLiteralCodeStatements("return resultEntity;");

            yield return new ClassMethodBuilder()
                .WithName("UpdateAfterRead")
                .WithTypeName(instance.GetEntityBuilderFullName())
                .AddParameter("resultEntity", instance.GetEntityBuilderFullName())
                .AddParameter("reader", typeof(IDataReader))
                .AddLiteralCodeStatements(outputFields.Select(x => CreateAfterReadStatement(x, instance, entityClassType.IsImmutable(), string.Empty)))
                .AddLiteralCodeStatements(outputFieldsForOriginal.Select(x => CreateAfterReadStatement(x, instance, entityClassType.IsImmutable(), "Original")))
                .AddCodeStatements(instance.Metadata.GetValues<ICodeStatement>(CommandEntityProviders.UpdateAfterReadStatement).Select(x => x.CreateBuilder()))
                .AddLiteralCodeStatements("return resultEntity;");
        }

        if (!instance.Metadata.GetBooleanValue(CommandProviders.PreventDelete))
        {
            yield return new ClassMethodBuilder()
                .WithName("DeleteResultEntity")
                .WithTypeName(instance.GetEntityBuilderFullName())
                .AddParameter("resultEntity", instance.GetEntityBuilderFullName())
                .AddCodeStatements(instance.Metadata.GetValues<ICodeStatement>(CommandEntityProviders.DeleteResultEntityStatement).Select(x => x.CreateBuilder()))
                .AddLiteralCodeStatements("return resultEntity;");

            yield return new ClassMethodBuilder()
                .WithName("DeleteAfterRead")
                .WithTypeName(instance.GetEntityBuilderFullName())
                .AddParameter("resultEntity", instance.GetEntityBuilderFullName())
                .AddParameter("reader", typeof(IDataReader))
                .AddLiteralCodeStatements(outputFields.Select(x => CreateAfterReadStatement(x, instance, entityClassType.IsImmutable(), string.Empty)))
                .AddLiteralCodeStatements(outputFieldsForOriginal.Select(x => CreateAfterReadStatement(x, instance, entityClassType.IsImmutable(), "Original")))
                .AddCodeStatements(instance.Metadata.GetValues<ICodeStatement>(CommandEntityProviders.DeleteAfterReadStatement).Select(x => x.CreateBuilder()))
                .AddLiteralCodeStatements("return resultEntity;");
        }
    }

    private static string CreateAfterReadStatement(IFieldInfo field, IDataObjectInfo instance, bool isImmutable, string suffix)
        => isImmutable
            ? $"resultEntity = resultEntity.Set{field.CreatePropertyName(instance)}{suffix}(reader.{field.GetSqlReaderMethodName()}(\"{field.GetDatabaseFieldName()}\"));"
            : $"resultEntity.{field.CreatePropertyName(instance)}{suffix} = reader.{field.GetSqlReaderMethodName()}(\"{field.GetDatabaseFieldName()}\");";
}
