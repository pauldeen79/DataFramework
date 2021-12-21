using System.Linq;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Database.Builders;
using ModelFramework.Database.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoBuilderExtensions
    {
        public static DataObjectInfoBuilder WithConcurrencyCheckBehavior(this DataObjectInfoBuilder instance, ConcurrencyCheckBehavior concurrencyCheckBehavior)
            => instance.ReplaceMetadata(Database.ConcurrencyCheckBehavior, concurrencyCheckBehavior);

        public static DataObjectInfoBuilder WithAddStoredProcedureName(this DataObjectInfoBuilder instance, string? storedProcedureName)
            => instance.ReplaceMetadata(Database.AddStoredProcedureName, storedProcedureName);

        public static DataObjectInfoBuilder AddAddStoredProcedureStatements(this DataObjectInfoBuilder instance, params ISqlStatement[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Database.AddStoredProcedureStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddAddStoredProcedureStatements(this DataObjectInfoBuilder instance, params ISqlStatementBuilder[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Database.AddStoredProcedureStatement).WithValue(x.Build())));

        public static DataObjectInfoBuilder WithUpdateStoredProcedureName(this DataObjectInfoBuilder instance, string? storedProcedureName)
            => instance.ReplaceMetadata(Database.UpdateStoredProcedureName, storedProcedureName);

        public static DataObjectInfoBuilder AddUpdateStoredProcedureStatements(this DataObjectInfoBuilder instance, params ISqlStatement[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Database.UpdateStoredProcedureStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddUpdateStoredProcedureStatements(this DataObjectInfoBuilder instance, params ISqlStatementBuilder[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Database.UpdateStoredProcedureStatement).WithValue(x.Build())));

        public static DataObjectInfoBuilder WithDeleteStoredProcedureName(this DataObjectInfoBuilder instance, string? storedProcedureName)
            => instance.ReplaceMetadata(Database.DeleteStoredProcedureName, storedProcedureName);

        public static DataObjectInfoBuilder AddDeleteStoredProcedureStatements(this DataObjectInfoBuilder instance, params ISqlStatement[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Database.DeleteStoredProcedureStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddDeleteStoredProcedureStatements(this DataObjectInfoBuilder instance, params ISqlStatementBuilder[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Database.DeleteStoredProcedureStatement).WithValue(x.Build())));

        public static DataObjectInfoBuilder WithCustomAddCommandText(this DataObjectInfoBuilder instance, string? commandText)
            => instance.ReplaceMetadata(Database.AddCustomCommandText, commandText);

        public static DataObjectInfoBuilder WithCustomUpdateCommandText(this DataObjectInfoBuilder instance, string? commandText)
            => instance.ReplaceMetadata(Database.UpdateCustomCommandText, commandText);

        public static DataObjectInfoBuilder WithCustomDeleteCommandText(this DataObjectInfoBuilder instance, string? commandText)
            => instance.ReplaceMetadata(Database.DeleteCustomCommandText, commandText);

        public static DataObjectInfoBuilder WithDatabaseTableName(this DataObjectInfoBuilder instance, string? tableName)
            => instance.ReplaceMetadata(Database.TableName, tableName);

        public static DataObjectInfoBuilder WithDatabaseSchemaName(this DataObjectInfoBuilder instance, string? schemaName)
            => instance.ReplaceMetadata(Database.SchemaName, schemaName);

        public static DataObjectInfoBuilder WithDatabaseFileGroupName(this DataObjectInfoBuilder instance, string? fileGroupName)
            => instance.ReplaceMetadata(Database.FileGroupName, fileGroupName);

        public static DataObjectInfoBuilder AddViews(this DataObjectInfoBuilder instance, params IView[] views)
            => instance.AddMetadata(views.Select(x => new MetadataBuilder().WithName(Database.View).WithValue(x)));

        public static DataObjectInfoBuilder AddViews(this DataObjectInfoBuilder instance, params ViewBuilder[] views)
            => instance.AddMetadata(views.Select(x => new MetadataBuilder().WithName(Database.View).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddAdditionalTables(this DataObjectInfoBuilder instance, params ITable[] tables)
            => instance.AddMetadata(tables.Select(x => new MetadataBuilder().WithName(Database.AdditionalTable).WithValue(x)));

        public static DataObjectInfoBuilder AddAdditionalTables(this DataObjectInfoBuilder instance, params TableBuilder[] tables)
            => instance.AddMetadata(tables.Select(x => new MetadataBuilder().WithName(Database.AdditionalTable).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddAdditionalStoredProcedures(this DataObjectInfoBuilder instance, params IStoredProcedure[] storedProcedures)
            => instance.AddMetadata(storedProcedures.Select(x => new MetadataBuilder().WithName(Database.AdditionalStoredProcedure).WithValue(x)));

        public static DataObjectInfoBuilder AddAdditionalStoredProcedures(this DataObjectInfoBuilder instance, params StoredProcedureBuilder[] storedProcedures)
            => instance.AddMetadata(storedProcedures.Select(x => new MetadataBuilder().WithName(Database.AdditionalStoredProcedure).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddIndexes(this DataObjectInfoBuilder instance, params IIndex[] indexes)
            => instance.AddMetadata(indexes.Select(x => new MetadataBuilder().WithName(Database.Index).WithValue(x)));

        public static DataObjectInfoBuilder AddIndexes(this DataObjectInfoBuilder instance, params IndexBuilder[] indexes)
            => instance.AddMetadata(indexes.Select(x => new MetadataBuilder().WithName(Database.Index).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddPrimaryKeyConstraints(this DataObjectInfoBuilder instance, params IPrimaryKeyConstraint[] primaryKeyConstraints)
            => instance.AddMetadata(primaryKeyConstraints.Select(x => new MetadataBuilder().WithName(Database.PrimaryKeyConstraint).WithValue(x)));

        public static DataObjectInfoBuilder AddPrimaryKeyConstraints(this DataObjectInfoBuilder instance, params PrimaryKeyConstraintBuilder[] primaryKeyConstraints)
            => instance.AddMetadata(primaryKeyConstraints.Select(x => new MetadataBuilder().WithName(Database.PrimaryKeyConstraint).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddForeignKeyConstraints(this DataObjectInfoBuilder instance, params IForeignKeyConstraint[] foreignKeyConstraints)
            => instance.AddMetadata(foreignKeyConstraints.Select(x => new MetadataBuilder().WithName(Database.ForeignKeyConstraint).WithValue(x)));

        public static DataObjectInfoBuilder AddForeignKeyConstraints(this DataObjectInfoBuilder instance, params ForeignKeyConstraintBuilder[] foreignKeyConstraints)
            => instance.AddMetadata(foreignKeyConstraints.Select(x => new MetadataBuilder().WithName(Database.ForeignKeyConstraint).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddCheckConstraints(this DataObjectInfoBuilder instance, params ICheckConstraint[] checkConstraints)
            => instance.AddMetadata(checkConstraints.Select(x => new MetadataBuilder().WithName(Database.CheckConstraint).WithValue(x)));

        public static DataObjectInfoBuilder AddCheckConstraints(this DataObjectInfoBuilder instance, params CheckConstraintBuilder[] checkConstraints)
            => instance.AddMetadata(checkConstraints.Select(x => new MetadataBuilder().WithName(Database.CheckConstraint).WithValue(x.Build())));
    }
}
