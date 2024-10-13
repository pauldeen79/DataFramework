namespace DataFramework.Pipelines.QueryBuilder.Components;

public class SetNameComponentBuilder : IQueryBuilderComponentBuilder
{
    public IPipelineComponent<QueryBuilderContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<QueryBuilderContext>
{
    public Task<Result> Process(PipelineContext<QueryBuilderContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName($"{context.Request.SourceModel.Name}QueryBuilder")
            .WithNamespace(context.Request.Settings.QueryBuilderNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
