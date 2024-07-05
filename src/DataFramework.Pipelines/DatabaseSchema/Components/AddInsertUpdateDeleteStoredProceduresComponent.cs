namespace DataFramework.Pipelines.DatabaseSchema.Components;

public class AddInsertUpdateDeleteStoredProceduresComponentBuilder : IDatabaseSchemaComponentBuilder
{
    public IPipelineComponent<DatabaseSchemaContext> Build()
        => new AddInsertUpdateDeleteStoredProceduresComponent();
}

public class AddInsertUpdateDeleteStoredProceduresComponent : IPipelineComponent<DatabaseSchemaContext>
{
    public Task<Result> Process(PipelineContext<DatabaseSchemaContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builders.AddRange(context.Request.SourceModel.GetStoredProcedures(context.Request.Settings));

        return Task.FromResult(Result.Continue());
    }
}
