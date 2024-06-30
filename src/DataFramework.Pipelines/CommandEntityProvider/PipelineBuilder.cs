namespace DataFramework.Pipelines.CommandEntityProvider;

public class PipelineBuilder : PipelineBuilder<CommandEntityProviderContext>
{
    public PipelineBuilder(IEnumerable<ICommandEntityProviderComponentBuilder> commandEntityProviderComponentBuilders)
    {
        AddComponents(commandEntityProviderComponentBuilders);
    }
}
