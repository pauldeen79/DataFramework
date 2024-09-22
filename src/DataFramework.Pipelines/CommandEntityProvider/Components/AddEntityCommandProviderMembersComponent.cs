namespace DataFramework.Pipelines.CommandEntityProvider.Components;

public class AddEntityCommandProviderMembersComponentBuilder : ICommandEntityProviderComponentBuilder
{
    private readonly ICsharpExpressionDumper _csharpExpressionDumper;

    public AddEntityCommandProviderMembersComponentBuilder(ICsharpExpressionDumper csharpExpressionDumper)
    {
        _csharpExpressionDumper = csharpExpressionDumper.IsNotNull(nameof(csharpExpressionDumper));
    }

    public IPipelineComponent<CommandEntityProviderContext> Build()
        => new AddEntityCommandProviderMembersComponent(_csharpExpressionDumper);
}

public class AddEntityCommandProviderMembersComponent : IPipelineComponent<CommandEntityProviderContext>
{
    private const string ResultEntity = "resultEntity";
    private const string ReturnResultEntityStatement = "return resultEntity;";

    private readonly ICsharpExpressionDumper _csharpExpressionDumper;

    public AddEntityCommandProviderMembersComponent(ICsharpExpressionDumper csharpExpressionDumper)
    {
        _csharpExpressionDumper = csharpExpressionDumper.IsNotNull(nameof(csharpExpressionDumper));
    }

    public Task<Result> Process(PipelineContext<CommandEntityProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddInterfaces(context.Request.Settings.EntityClassType.IsImmutable()
                ? $"{typeof(IDatabaseCommandEntityProvider<,>).WithoutGenerics()}<{context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace)}, <{context.Request.SourceModel.GetEntityBuilderFullName(context.Request.Settings.DefaultEntityNamespace, context.Request.Settings.DefaultBuilderNamespace, context.Request.Settings.EntityClassType.IsImmutable())}>"
                : $"{typeof(IDatabaseCommandEntityProvider<,>).WithoutGenerics()}<{context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace)}>")
            .AddProperties(GetEntityCommandProviderClassProperties(context.Request.SourceModel, context.Request.Settings))
            .AddMethods(GetEntityCommandProviderClassMethods(context.Request.SourceModel, context.Request.Settings));

        return Task.FromResult(Result.Continue());
    }

    private static IEnumerable<PropertyBuilder> GetEntityCommandProviderClassProperties(DataObjectInfo instance, PipelineSettings settings)
    {
        yield return new PropertyBuilder()
            .WithName($"{nameof(IDatabaseCommandEntityProvider<object, string>.CreateResultEntity)}")
            .WithTypeName($"{typeof(CreateResultEntityHandler<>).WithoutGenerics()}<{instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable())}, {typeof(DatabaseOperation).FullName}, {instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable())}>")
            .WithIsNullable()
            .WithHasSetter(false)
            .AddGetterStringCodeStatements
            (
                "return (entity, operation) =>",
                "{",
                "    switch (operation)",
                "    {"
            )
            .AddEntityCommandProviderMethod(instance, settings.CommandProviderEnableAdd, DatabaseOperation.Insert, "ResultEntity")
            .AddEntityCommandProviderMethod(instance, settings.CommandProviderEnableUpdate, DatabaseOperation.Update, "ResultEntity")
            .AddEntityCommandProviderMethod(instance, settings.CommandProviderEnableDelete, DatabaseOperation.Delete, "ResultEntity")
            .AddGetterStringCodeStatements
            (
                "         default:",
                $"             throw new {typeof(ArgumentOutOfRangeException).FullName}(\"operation\", string.Format(\"Unsupported operation: {{0}}\", operation));",
                "    }",
                "};"
            );

        yield return new PropertyBuilder()
            .WithName($"{nameof(IDatabaseCommandEntityProvider<object, string>.AfterRead)}")
            .WithTypeName($"{typeof(AfterReadHandler<>).WithoutGenerics()}<{instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable())}, {typeof(DatabaseOperation).FullName}, {typeof(IDataReader).FullName}, {instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable())}>")
            .WithIsNullable()
            .WithHasSetter(false)
            .AddGetterStringCodeStatements
            (
                "return (entity, operation, reader) =>",
                "{",
                "    switch (operation)",
                "    {"
            )
            .AddEntityCommandProviderMethod(instance, settings.CommandProviderEnableAdd, DatabaseOperation.Insert, "AfterRead")
            .AddEntityCommandProviderMethod(instance, settings.CommandProviderEnableUpdate, DatabaseOperation.Update, "AfterRead")
            .AddEntityCommandProviderMethod(instance, settings.CommandProviderEnableDelete, DatabaseOperation.Delete, "AfterRead")
            .AddGetterStringCodeStatements
            (
                "         default:",
                $"             throw new {typeof(ArgumentOutOfRangeException).FullName}(\"operation\", string.Format(\"Unsupported operation: {{0}}\", operation));",
                "    }",
                "};"
            );

        yield return new PropertyBuilder()
            .WithName($"{nameof(IDatabaseCommandEntityProvider<object, string>.CreateBuilder)}")
            .WithTypeName($"{typeof(CreateBuilderHandler<,>).WithoutGenerics()}<{instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable())}, {instance.GetEntityFullName(settings.DefaultEntityNamespace)}>")
            .WithIsNullable()
            .WithHasSetter(false)
            .AddGetterStringCodeStatements(settings.EntityClassType.IsImmutable()
                ? $"return entity => new {instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable())}(entity);"
                : "return entity;");

        yield return new PropertyBuilder()
            .WithName($"{nameof(IDatabaseCommandEntityProvider<object, string>.CreateEntity)}")
            .WithTypeName($"{typeof(CreateEntityHandler<,>).WithoutGenerics()}<{instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable())}, {instance.GetEntityFullName(settings.DefaultEntityNamespace)}>")
            .WithIsNullable()
            .WithHasSetter(false)
            .AddGetterStringCodeStatements("return builder => builder.Build();");
    }

    private IEnumerable<MethodBuilder> GetEntityCommandProviderClassMethods(DataObjectInfo instance, PipelineSettings settings)
    {
        var outputFields = instance.GetOutputFields(settings.ConcurrencyCheckBehavior).ToArray();
        var originalFields = instance.GetUpdateConcurrencyCheckFields(settings.ConcurrencyCheckBehavior).ToArray();
        var outputFieldsForOriginal = outputFields.Where(x => originalFields.Contains(x)).ToArray();

        if (settings.CommandProviderEnableAdd)
        {
            yield return new MethodBuilder()
                .WithName("AddResultEntity")
                .WithVisibility(Visibility.Private)
                .WithReturnTypeName(instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable()))
                .AddParameter(ResultEntity, instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable()))
                .AddCodeStatements(settings.CommandEntityProviderAddResultEntityStatements.Select(x => x.ToBuilder()))
                .AddStringCodeStatements(ReturnResultEntityStatement);

            yield return new MethodBuilder()
                .WithName("AddAfterRead")
                .WithVisibility(Visibility.Private)
                .WithReturnTypeName(instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable()))
                .AddParameter(ResultEntity, instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable()))
                .AddParameter("reader", typeof(IDataReader))
                .AddStringCodeStatements(outputFields.Select(x => CreateAfterReadStatement(x, instance, settings.EntityClassType.IsImmutable(), string.Empty)))
                .AddStringCodeStatements(outputFieldsForOriginal.Select(x => CreateAfterReadStatement(x, instance, settings.EntityClassType.IsImmutable(), "Original")))
                .AddCodeStatements(settings.CommandEntityProviderAddAfterReadStatements.Select(x => x.ToBuilder()))
                .AddStringCodeStatements(ReturnResultEntityStatement);
        }

        if (settings.CommandProviderEnableUpdate)
        {
            yield return new MethodBuilder()
                .WithName("UpdateResultEntity")
                .WithVisibility(Visibility.Private)
                .WithReturnTypeName(instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable()))
                .AddParameter(ResultEntity, instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable()))
                .AddCodeStatements(settings.CommandEntityProviderUpdateResultEntityStatements.Select(x => x.ToBuilder()))
                .AddStringCodeStatements(ReturnResultEntityStatement);

            yield return new MethodBuilder()
                .WithName("UpdateAfterRead")
                .WithVisibility(Visibility.Private)
                .WithReturnTypeName(instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable()))
                .AddParameter(ResultEntity, instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable()))
                .AddParameter("reader", typeof(IDataReader))
                .AddStringCodeStatements(outputFields.Select(x => CreateAfterReadStatement(x, instance, settings.EntityClassType.IsImmutable(), string.Empty)))
                .AddStringCodeStatements(outputFieldsForOriginal.Select(x => CreateAfterReadStatement(x, instance, settings.EntityClassType.IsImmutable(), "Original")))
                .AddCodeStatements(settings.CommandEntityProviderUpdateAfterReadStatements.Select(x => x.ToBuilder()))
                .AddStringCodeStatements(ReturnResultEntityStatement);
        }

        if (settings.CommandProviderEnableDelete)
        {
            yield return new MethodBuilder()
                .WithName("DeleteResultEntity")
                .WithVisibility(Visibility.Private)
                .WithReturnTypeName(instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable()))
                .AddParameter(ResultEntity, instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable()))
                .AddCodeStatements(settings.CommandEntityProviderDeleteResultEntityStatements.Select(x => x.ToBuilder()))
                .AddStringCodeStatements(ReturnResultEntityStatement);

            yield return new MethodBuilder()
                .WithName("DeleteAfterRead")
                .WithVisibility(Visibility.Private)
                .WithReturnTypeName(instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable()))
                .AddParameter(ResultEntity, instance.GetEntityBuilderFullName(settings.DefaultEntityNamespace, settings.DefaultBuilderNamespace, settings.EntityClassType.IsImmutable()))
                .AddParameter("reader", typeof(IDataReader))
                .AddStringCodeStatements(outputFields.Select(x => CreateAfterReadStatement(x, instance, settings.EntityClassType.IsImmutable(), string.Empty)))
                .AddStringCodeStatements(outputFieldsForOriginal.Select(x => CreateAfterReadStatement(x, instance, settings.EntityClassType.IsImmutable(), "Original")))
                .AddCodeStatements(settings.CommandEntityProviderDeleteAfterReadStatements.Select(x => x.ToBuilder()))
                .AddStringCodeStatements(ReturnResultEntityStatement);
        }
    }

    private string CreateAfterReadStatement(FieldInfo field, DataObjectInfo instance, bool isImmutable, string suffix)
        => isImmutable
            ? $"resultEntity = resultEntity.Set{field.CreatePropertyName(instance)}{suffix}{typeof(CrossCutting.Data.Sql.Extensions.DataReaderExtensions).FullName}{field.SqlReaderMethodName}(reader, {_csharpExpressionDumper.Dump(field.GetDatabaseFieldName())}));"
            : $"resultEntity.{field.CreatePropertyName(instance)}{suffix} = {typeof(CrossCutting.Data.Sql.Extensions.DataReaderExtensions).FullName}.{field.SqlReaderMethodName}(reader, {_csharpExpressionDumper.Dump(field.GetDatabaseFieldName())});";
}
