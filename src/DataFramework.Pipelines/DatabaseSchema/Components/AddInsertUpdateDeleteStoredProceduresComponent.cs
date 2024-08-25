namespace DataFramework.Pipelines.DatabaseSchema.Components;

public class AddInsertUpdateDeleteStoredProceduresComponentBuilder : IDatabaseSchemaComponentBuilder
{
    private readonly IFormattableStringParser _formattableStringParser;

    public AddInsertUpdateDeleteStoredProceduresComponentBuilder(IFormattableStringParser formattableStringParser)
    {
        _formattableStringParser = formattableStringParser.IsNotNull(nameof(formattableStringParser));
    }

    public IPipelineComponent<DatabaseSchemaContext> Build()
        => new AddInsertUpdateDeleteStoredProceduresComponent(_formattableStringParser);
}

public class AddInsertUpdateDeleteStoredProceduresComponent : IPipelineComponent<DatabaseSchemaContext>
{
    private readonly IFormattableStringParser _formattableStringParser;

    public AddInsertUpdateDeleteStoredProceduresComponent(IFormattableStringParser formattableStringParser)
    {
        _formattableStringParser = formattableStringParser.IsNotNull(nameof(formattableStringParser));
    }

    public Task<Result> Process(PipelineContext<DatabaseSchemaContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builders.AddRange(context.Request.GetStoredProcedures(context.Request.Settings, context.Request.FormatProvider, _formattableStringParser));

        return Task.FromResult(Result.Continue());
    }
}
