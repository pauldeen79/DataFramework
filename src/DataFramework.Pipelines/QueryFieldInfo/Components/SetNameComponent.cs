namespace DataFramework.Pipelines.QueryFieldInfo.Components;

public class SetNameComponentBuilder : IQueryFieldInfoComponentBuilder
{
    public IPipelineComponent<QueryFieldInfoContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<QueryFieldInfoContext>
{
    public Task<Result> Process(PipelineContext<QueryFieldInfoContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName($"{context.Request.SourceModel.Name}Query")
            .WithNamespace(context.Request.Settings.QueryFieldInfoNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
