namespace DataFramework.Pipelines.CommandProvider.Components;

public class AddCommandProviderMembersComponentBuilder : ICommandProviderComponentBuilder
{
    public IPipelineComponent<CommandProviderContext> Build()
        => new AddCommandProviderMembersComponent();
}

public class AddCommandProviderMembersComponent : IPipelineComponent<CommandProviderContext>
{
    public Task<Result> Process(PipelineContext<CommandProviderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .AddInterfaces(typeof(IDatabaseCommandProvider<>).ReplaceGenericTypeName(context.Request.SourceModel.GetEntityIdentityFullName(context.Request.Settings.DefaultIdentityNamespace)))
            .AddMethods(GetCommandProviderClassMethods(context));
        
        return Task.FromResult(Result.Continue());
    }

    private static IEnumerable<MethodBuilder> GetCommandProviderClassMethods(PipelineContext<CommandProviderContext> context)
    {
        yield return new MethodBuilder()
            .WithName(nameof(IDatabaseCommandProvider<object>.Create))
            .WithReturnType(typeof(IDatabaseCommand))
            .AddParameter("source", context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace))
            .AddParameter("operation", typeof(DatabaseOperation))
            .AddStringCodeStatements
            (
                "switch (operation)",
                "{"
            )
            .AddCommandProviderMethod(context.Request.SourceModel, context.Request.Settings.CommandProviderEnableAdd, DatabaseOperation.Insert, GetInsertCommandType(context), GetInsertCommand(context))
            .AddCommandProviderMethod(context.Request.SourceModel, context.Request.Settings.CommandProviderEnableUpdate, DatabaseOperation.Update, GetUpdateCommandType(context), GetUpdateCommand(context))
            .AddCommandProviderMethod(context.Request.SourceModel, context.Request.Settings.CommandProviderEnableDelete, DatabaseOperation.Delete, GetDeleteCommandType(context), GetDeleteCommand(context))
            .AddStringCodeStatements
            (
                "    default:",
                $@"        throw new {typeof(ArgumentOutOfRangeException).FullName}(""operation"", string.Format(""Unsupported operation: {{0}}"", operation));",
                "}"
            );

        if (context.Request.Settings.CommandProviderEnableAdd)
        {
            yield return new MethodBuilder()
                .WithName("AddParameters")
                .WithReturnType(typeof(object))
                .AddParameter("resultEntity", context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace))
                .AddStringCodeStatements("return new[]", "{")
                .AddStringCodeStatements(context.Request.SourceModel.Fields.Where(x => x.UseOnInsert).Select(x => $"    new {typeof(KeyValuePair<,>).WithoutGenerics()}<{typeof(string).FullName}, {GetObjectType(context.Request.Settings)}>(\"@{x.Name}\", resultEntity.{x.Name}),"))
                .AddStringCodeStatements("};");
        }

        if (context.Request.Settings.CommandProviderEnableUpdate)
        {
            yield return new MethodBuilder()
                .WithName("UpdateParameters")
                .WithReturnType(typeof(object))
                .AddParameter("resultEntity", context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace))
                .AddStringCodeStatements("return new[]", "{")
                .AddStringCodeStatements(context.Request.SourceModel.Fields.Where(x => x.UseOnUpdate).Select(x => $"    new {typeof(KeyValuePair<,>).WithoutGenerics()}<{typeof(string).FullName}, {GetObjectType(context.Request.Settings)}>(\"@{x.Name}\", resultEntity.{x.Name}),"))
                .AddStringCodeStatements(context.Request.SourceModel.GetUpdateConcurrencyCheckFields(context.Request.Settings.ConcurrencyCheckBehavior).Where(x => x.UseOnUpdate || x.IsIdentityField || x.IsDatabaseIdentityField).Select(x => $"    new {typeof(KeyValuePair<,>).WithoutGenerics()}<{typeof(string).FullName}, {GetObjectType(context.Request.Settings)}>(\"@{x.Name}Original\", resultEntity.{x.Name}Original),"))
                .AddStringCodeStatements("};");
        }

        if (context.Request.Settings.CommandProviderEnableDelete)
        {
            yield return new MethodBuilder()
                .WithName("DeleteParameters")
                .WithReturnType(typeof(object))
                .AddParameter("resultEntity", context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace))
                .AddStringCodeStatements("return new[]", "{")
                .AddStringCodeStatements(context.Request.SourceModel.GetUpdateConcurrencyCheckFields(context.Request.Settings.ConcurrencyCheckBehavior).Where(x => x.UseOnDelete || x.IsIdentityField || x.IsDatabaseIdentityField).Select(x => $"    new {typeof(KeyValuePair<,>).WithoutGenerics()}<{typeof(string).FullName}, {GetObjectType(context.Request.Settings)}>(\"@{x.Name}Original\", resultEntity.{x.Name}Original),"))
                .AddStringCodeStatements("};");
        }
    }

    private static string GetObjectType(PipelineSettings settings)
        => settings.EnableNullableContext
            ? $"{typeof(object).FullName}?"
            : typeof(object).FullName;

    private static string GetInsertCommandType(PipelineContext<CommandProviderContext> context)
        => context.Request.Settings.UseAddStoredProcedure
            ? typeof(StoredProcedureCommand<>).ReplaceGenericTypeName(context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace))
            : typeof(TextCommand<>).ReplaceGenericTypeName(context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace));

    private static string GetInsertCommand(PipelineContext<CommandProviderContext> context)
        => context.Request.Settings.UseAddStoredProcedure
            //TODO: Add support for named format strings here
            ? $"[{string.Format(context.Request.Settings.AddStoredProcedureName, context.Request.SourceModel.GetDatabaseTableName())}]"
            : context.Request.SourceModel.CreateDatabaseInsertCommandText(context.Request.Settings.ConcurrencyCheckBehavior);

    private static string GetUpdateCommandType(PipelineContext<CommandProviderContext> context)
        => context.Request.Settings.UseUpdateStoredProcedure
            ? typeof(StoredProcedureCommand<>).ReplaceGenericTypeName(context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace))
            : typeof(TextCommand<>).ReplaceGenericTypeName(context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace));

    private static string GetUpdateCommand(PipelineContext<CommandProviderContext> context)
        => context.Request.Settings.UseUpdateStoredProcedure
            //TODO: Add support for named format strings here
            ? $"[{string.Format(context.Request.Settings.UpdateStoredProcedureName, context.Request.SourceModel.GetDatabaseTableName())}]"
            : context.Request.SourceModel.CreateDatabaseUpdateCommandText(context.Request.Settings.ConcurrencyCheckBehavior);

    private static string GetDeleteCommandType(PipelineContext<CommandProviderContext> context)
        => context.Request.Settings.UseDeleteStoredProcedure
            ? typeof(StoredProcedureCommand<>).ReplaceGenericTypeName(context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace))
            : typeof(TextCommand<>).ReplaceGenericTypeName(context.Request.SourceModel.GetEntityFullName(context.Request.Settings.DefaultEntityNamespace));

    private static string GetDeleteCommand(PipelineContext<CommandProviderContext> context)
        => context.Request.Settings.UseDeleteStoredProcedure
            //TODO: Add support for named format strings here
            ? $"[{string.Format(context.Request.Settings.DeleteStoredProcedureName, context.Request.SourceModel.GetDatabaseTableName())}]"
            : context.Request.SourceModel.CreateDatabaseDeleteCommandText(context.Request.Settings.ConcurrencyCheckBehavior);
}
