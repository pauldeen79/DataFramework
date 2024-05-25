namespace DataFramework.Pipelines.Entity.Components;

public class AddAttributesComponentBuilder : IEntityComponentBuilder
{
    public IPipelineComponent<EntityContext> Build()
        => new AddAttributesComponent();
}

public class AddAttributesComponent : IPipelineComponent<EntityContext>
{
    public Task<Result> Process(PipelineContext<EntityContext> context, CancellationToken token)
    {
        context = context.IsNotNull(nameof(context));

        if (!context.Request.Settings.AddComponentModelAttributes)
        {
            return Task.FromResult(Result.Continue());
        }

        if (context.Request.SourceModel.IsReadOnly)
        {
            context.Request.Builder.AddAttributes(new AttributeBuilder().AddNameAndParameter("System.ComponentModel.ReadOnly", true));
        }

        if (context.Request.SourceModel.Description is not null && !string.IsNullOrEmpty(context.Request.SourceModel.Description))
        {
            context.Request.Builder.AddAttributes(new AttributeBuilder().AddNameAndParameter("System.ComponentModel.Description", context.Request.SourceModel.Description));
        }

        if (context.Request.SourceModel.DisplayName is not null && !string.IsNullOrEmpty(context.Request.SourceModel.DisplayName))
        {
            context.Request.Builder.AddAttributes(new AttributeBuilder().AddNameAndParameter("System.ComponentModel.DisplayName", context.Request.SourceModel.DisplayName));
        }

        if (!context.Request.SourceModel.IsVisible)
        {
            context.Request.Builder.AddAttributes(new AttributeBuilder().AddNameAndParameter("System.ComponentModel.Browsable", false));
        }

        return Task.FromResult(Result.Continue());
    }
}
