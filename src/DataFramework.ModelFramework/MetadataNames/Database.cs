namespace DataFramework.ModelFramework.MetadataNames;

internal static class Database
{
    internal const string Root = Base.Root + ".Database";

    internal const string SchemaName = Root + ".Schema";
    internal const string FileGroupName = Root + ".FileGroup";
    internal const string TableName = Root + ".TableName";
    internal const string ConcurrencyCheckBehavior = Root + ".ConcurrencyCheckBehavior";
    internal const string SkipFieldOnFind = Root + ".SkipFieldOnFind";
    internal const string FieldName = Root + ".FieldName";
    internal const string IsRowVersion = Root + ".IsRowVersion";
    internal const string IsMaxLength = Root + ".IsMaxLength";
    internal const string IsRequired = Root + ".IsRequired";
    internal const string SqlFieldType = Root + ".FieldType";
    internal const string IdentityField = Root + ".IdentityField";
    internal const string NumericPrecision = Root + ".NumericPrecision";
    internal const string NumericScale = Root + ".NumericScale";
    internal const string SqlStringCollation = Root + ".StringCollation";
    internal const string SqlStringLength = Root + ".StringLength";
    internal const string CheckConstraintExpression = Root + ".CheckConstraintExpression";
    internal const string UseOnInsert = Root + ".UseOnInsert";
    internal const string UseOnUpdate = Root + ".UseOnUpdate";
    internal const string UseOnDelete = Root + ".UseOnDelete";
    internal const string UseOnSelect = Root + ".UseOnSelect";
    internal const string SqlReaderMethodName = Root + ".SqlReaderMethodName";
    internal const string HasAddStoredProcedure = Root + ".HasAddStoredProcedure";
    internal const string HasUpdateStoredProcedure = Root + ".HasUpdateStoredProcedure";
    internal const string HasDeleteStoredProcedure = Root + ".HasDeleteStoredProcedure";
    internal const string AddStoredProcedureName = Root + ".AddStoredProcedureName";
    internal const string AddStoredProcedureStatement = Root + ".AddStoredProcedureStatement";
    internal const string AddCustomCommandText = Root + ".AddCustomCommandText";
    internal const string UpdateStoredProcedureName = Root + ".UpdateStoredProcedureName";
    internal const string UpdateStoredProcedureStatement = Root + ".UpdateStoredProcedureStatement";
    internal const string UpdateCustomCommandText = Root + ".UpdateCustomCommandText";
    internal const string DeleteStoredProcedureName = Root + ".DeleteStoredProcedureName";
    internal const string DeleteStoredProcedureStatement = Root + ".DeleteStoredProcedureStatement";
    internal const string DeleteCustomCommandText = Root + ".DeleteCustomCommandText";
    internal const string View = Root + ".View";
    internal const string AdditionalTable = Root + ".CustomTable";
    internal const string AdditionalStoredProcedure = Root + ".AdditionalStoredProcedure";
    internal const string Index = Root + ".Index";
    internal const string PrimaryKeyConstraint = Root + ".PrimaryKeyConstraint";
    internal const string ForeignKeyConstraint = Root + ".ForeignKeyConstraint";
    internal const string CheckConstraint = Root + ".CheckConstraint";

    internal const string CommandTypeForInsertTextName = Root + ".CommandTypeForInsert.Text";
    internal const string CommandTypeForUpdateTextName = Root + ".CommandTypeForUpdate.Text";
    internal const string CommandTypeForDeleteTextName = Root + ".CommandTypeForDelete.Text";

    internal const string CommandTypeForInsertParametersName = Root + ".CommandTypeForInsert.Parameters";
    internal const string CommandTypeForUpdateParametersName = Root + ".CommandTypeForUpdate.Parameters";
    internal const string CommandTypeForDeleteParametersName = Root + ".CommandTypeForDelete.Parameters";
}
