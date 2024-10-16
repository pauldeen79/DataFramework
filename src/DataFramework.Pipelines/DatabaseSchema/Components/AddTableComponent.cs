namespace DataFramework.Pipelines.DatabaseSchema.Components;

public class AddTableComponentBuilder : IDatabaseSchemaComponentBuilder
{
    public IPipelineComponent<DatabaseSchemaContext> Build()
        => new AddTableComponent();
}

public class AddTableComponent : IPipelineComponent<DatabaseSchemaContext>
{
    public Task<Result> Process(PipelineContext<DatabaseSchemaContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builders.Add(new TableBuilder()
            .WithName(context.Request.SourceModel.GetDatabaseTableName())
            .WithFileGroupName(context.Request.SourceModel.DatabaseFileGroupName)
            .WithSchema(context.Request.SourceModel.GetDatabaseSchemaName())
            .AddFields(context.Request.SourceModel.GetTableFields())
            .AddPrimaryKeyConstraints(context.Request.SourceModel.GetTablePrimaryKeyConstraints())
            .AddDefaultValueConstraints(context.Request.SourceModel.GetTableDefaultValueConstraints())
            .AddForeignKeyConstraints(context.Request.SourceModel.GetTableForeignKeyConstraints())
            .AddIndexes(context.Request.SourceModel.GetTableIndexes())
            .AddCheckConstraints(context.Request.SourceModel.GetTableCheckConstraints()
            ));

        return Task.FromResult(Result.Continue());
    }
}
