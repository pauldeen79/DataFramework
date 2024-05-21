namespace DataFramework.Pipelines.Entity.Components;

public class SetNameComponentBuilder : IEntityComponentBuilder
{
    public IPipelineComponent<EntityContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<EntityContext>
{
    public Task<Result> Process(PipelineContext<EntityContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName(context.Request.SourceModel.Name)
            .WithNamespace(context.Request.SourceModel.TypeName.GetNamespaceWithDefault(context.Request.Settings.DefaultEntityNamespace));

        return Task.FromResult(Result.Continue());
    }
}
