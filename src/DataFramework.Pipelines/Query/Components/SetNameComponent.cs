namespace DataFramework.Pipelines.Query.Components;

public class SetNameComponentBuilder : IQueryComponentBuilder
{
    public IPipelineComponent<QueryContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<QueryContext>
{
    public Task<Result> Process(PipelineContext<QueryContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName($"{context.Request.SourceModel.Name}Query")
            .WithNamespace(context.Request.Settings.QueryNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
