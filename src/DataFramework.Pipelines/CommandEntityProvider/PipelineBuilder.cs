namespace DataFramework.Pipelines.CommandEntityProvider;

public class PipelineBuilder : PipelineBuilder<CommandEntityProviderContext>
{
    public PipelineBuilder(IEnumerable<ICommandEntityProviderComponentBuilder> entityComponentBuilders)
    {
        AddComponents(entityComponentBuilders);
    }
}
