namespace DataFramework.Pipelines.EntityMapper.Components;

public class SetNameComponentBuilder : IEntityMapperComponentBuilder
{
    public IPipelineComponent<EntityMapperContext> Build()
        => new SetNameComponent();
}

public class SetNameComponent : IPipelineComponent<EntityMapperContext>
{
    public Task<Result> Process(PipelineContext<EntityMapperContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        context.Request.Builder
            .WithName($"{context.Request.SourceModel.Name}EntityMapper")
            .WithNamespace(context.Request.Settings.EntityMapperNamespace.WhenNullOrEmpty(() => context.Request.SourceModel.TypeName.GetNamespaceWithDefault()));

        return Task.FromResult(Result.Continue());
    }
}
