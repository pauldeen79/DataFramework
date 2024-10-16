namespace DataFramework.Pipelines.CommandProvider;

public class PipelineBuilder : PipelineBuilder<CommandProviderContext>
{
    public PipelineBuilder(IEnumerable<ICommandProviderComponentBuilder> commandProviderComponentBuilders)
    {
        AddComponents(commandProviderComponentBuilders);
    }
}
