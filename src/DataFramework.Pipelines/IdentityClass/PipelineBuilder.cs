namespace DataFramework.Pipelines.IdentityClass;

public class PipelineBuilder : PipelineBuilder<IdentityClassContext>
{
    public PipelineBuilder(IEnumerable<IIdentityClassComponentBuilder> entityComponentBuilders)
    {
        AddComponents(entityComponentBuilders);
    }
}
