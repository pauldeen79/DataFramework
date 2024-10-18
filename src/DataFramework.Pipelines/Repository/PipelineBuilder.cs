namespace DataFramework.Pipelines.Repository;

public class PipelineBuilder : PipelineBuilder<RepositoryContext>
{
    public PipelineBuilder(IEnumerable<IRepositoryComponentBuilder> repositoryComponentBuilders)
    {
        AddComponents(repositoryComponentBuilders);
    }
}
