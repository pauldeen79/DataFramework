namespace DataFramework.Pipelines.RepositoryInterface;

public class PipelineBuilder : PipelineBuilder<RepositoryInterfaceContext>
{
    public PipelineBuilder(IEnumerable<IRepositoryInterfaceComponentBuilder> repositoryInterfaceComponentBuilders)
    {
        AddComponents(repositoryInterfaceComponentBuilders);
    }
}
