namespace DataFramework.Pipelines.DependencyInjection.Components;

public class AddDependencyInjectionMembersComponentBuilder : IDependencyInjectionComponentBuilder
{
    public IPipelineComponent<DependencyInjectionContext> Build()
        => new AddDependencyInjectionMembersComponent();
}

public class AddDependencyInjectionMembersComponent : IPipelineComponent<DependencyInjectionContext>
{
    public Task<Result> Process(PipelineContext<DependencyInjectionContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        //context.Request.Builder

        return Task.FromResult(Result.Continue());
    }
}
