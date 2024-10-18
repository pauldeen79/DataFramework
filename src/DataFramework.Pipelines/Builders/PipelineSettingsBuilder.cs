namespace DataFramework.Pipelines.Builders;

public partial class PipelineSettingsBuilder
{
    public PipelineSettingsBuilder WithUseStoredProcedures(bool useStoredProcedures = true)
        => WithUseAddStoredProcedure(useStoredProcedures)
          .WithUseUpdateStoredProcedure(useStoredProcedures)
          .WithUseDeleteStoredProcedure(useStoredProcedures);

    partial void SetDefaultValues()
    {
        _addStoredProcedureName = @"Insert{DatabaseTableName}";
        _updateStoredProcedureName = @"Update{DatabaseTableName}";
        _deleteStoredProcedureName = @"Delete{DatabaseTableName}";
    }
}
