namespace DataFramework.Pipelines.IdentityCommandProvider;

public class PipelineBuilder : PipelineBuilder<IdentityCommandProviderContext>
{
    public PipelineBuilder(IEnumerable<IIdentityCommandProviderComponentBuilder> identityCommandProviderComponentBuilders)
    {
        AddComponents(identityCommandProviderComponentBuilders);
    }
}
