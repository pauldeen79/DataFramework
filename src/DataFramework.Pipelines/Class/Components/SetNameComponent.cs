namespace DataFramework.Pipelines.Class.Components;

public class SetNameComponentBuilder : IClassComponentBuilder
{
    public IPipelineComponent<ClassContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<ClassContext>
{
    public Task<Result> Process(PipelineContext<ClassContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName(context.Request.SourceModel.Name)
            .WithNamespace(context.Request.SourceModel.TypeName.GetNamespaceWithDefault(context.Request.Settings.DefaultEntityNamespace));

        return Task.FromResult(Result.Continue());
    }
}
