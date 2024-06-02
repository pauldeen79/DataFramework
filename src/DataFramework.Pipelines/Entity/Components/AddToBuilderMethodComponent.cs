namespace DataFramework.Pipelines.Entity.Components;

public class AddToBuilderMethodComponentBuilder : IEntityComponentBuilder
{
    public IPipelineComponent<EntityContext> Build()
        => new AddToBuilderMethodComponent();
}

public class AddToBuilderMethodComponent : IPipelineComponent<EntityContext>
{
    public Task<Result> Process(PipelineContext<EntityContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        if (!context.Request.Settings.AddToBuilderMethod)
        {
            return Task.FromResult(Result.Continue());
        }

        context.Request.Builder.AddMethods(new MethodBuilder()
            .WithName("ToBuilder")
            .WithReturnTypeName(context.Request.SourceModel.GetBuilderTypeName(context.Request.Settings))
            .AddStringCodeStatements($"return new {context.Request.SourceModel.GetBuilderTypeName(context.Request.Settings)}(this);")
            );

        return Task.FromResult(Result.Continue());
    }
}
