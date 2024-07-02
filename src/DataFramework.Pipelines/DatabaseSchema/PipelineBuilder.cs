namespace DataFramework.Pipelines.DatabaseSchema;

public class PipelineBuilder : PipelineBuilder<DatabaseSchemaContext>
{
    public PipelineBuilder(IEnumerable<IDatabaseSchemaComponentBuilder> databaseSchemaComponentBuilders)
    {
        AddComponents(databaseSchemaComponentBuilders);
    }
}
