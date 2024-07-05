namespace DataFramework.Pipelines.Builders;

public partial class PipelineSettingsBuilder
{
    public PipelineSettingsBuilder WithUseStoredProcedures(bool useStoredProcedures = true)
        => WithUseAddStoredProcedure(useStoredProcedures)
          .WithUseUpdateStoredProcedure(useStoredProcedures)
          .WithUseDeleteStoredProcedure(useStoredProcedures);

    partial void SetDefaultValues()
    {
        //TODO: Add support for named format strings here
        _addStoredProcedureName = @"Insert{0}";
        _updateStoredProcedureName = @"Update{0}";
        _deleteStoredProcedureName = @"Delete{0}";
    }
}
