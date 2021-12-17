using System.Collections.Generic;
using System.Linq;
using DataFramework.Core.Builders;
using DataFramework.ModelFramework.MetadataNames;
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

        public static DataObjectInfoBuilder AddAddStoredProcedureStatements(this DataObjectInfoBuilder instance, IEnumerable<ISqlStatement> statements)
            => instance.AddAddStoredProcedureStatements(statements.ToArray());

        public static DataObjectInfoBuilder AddAddStoredProcedureStatements(this DataObjectInfoBuilder instance, params ISqlStatementBuilder[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Database.AddStoredProcedureStatement).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddAddStoredProcedureStatements(this DataObjectInfoBuilder instance, IEnumerable<ISqlStatementBuilder> statements)
            => instance.AddAddStoredProcedureStatements(statements.ToArray());

        public static DataObjectInfoBuilder WithUpdateStoredProcedureName(this DataObjectInfoBuilder instance, string? storedProcedureName)
            => instance.ReplaceMetadata(Database.UpdateStoredProcedureName, storedProcedureName);

        public static DataObjectInfoBuilder AddUpdateStoredProcedureStatements(this DataObjectInfoBuilder instance, params ISqlStatement[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Database.UpdateStoredProcedureStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddUpdateStoredProcedureStatements(this DataObjectInfoBuilder instance, IEnumerable<ISqlStatement> statements)
            => instance.AddUpdateStoredProcedureStatements(statements.ToArray());

        public static DataObjectInfoBuilder AddUpdateStoredProcedureStatements(this DataObjectInfoBuilder instance, params ISqlStatementBuilder[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Database.UpdateStoredProcedureStatement).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddUpdateStoredProcedureStatements(this DataObjectInfoBuilder instance, IEnumerable<ISqlStatementBuilder> statements)
            => instance.AddUpdateStoredProcedureStatements(statements.ToArray());

        public static DataObjectInfoBuilder WithDeleteStoredProcedureName(this DataObjectInfoBuilder instance, string? storedProcedureName)
            => instance.ReplaceMetadata(Database.DeleteStoredProcedureName, storedProcedureName);

        public static DataObjectInfoBuilder AddDeleteStoredProcedureStatements(this DataObjectInfoBuilder instance, params ISqlStatement[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Database.DeleteStoredProcedureStatement).WithValue(x)));

        public static DataObjectInfoBuilder AddDeleteStoredProcedureStatements(this DataObjectInfoBuilder instance, IEnumerable<ISqlStatement> statements)
            => instance.AddDeleteStoredProcedureStatements(statements.ToArray());

        public static DataObjectInfoBuilder AddDeleteStoredProcedureStatements(this DataObjectInfoBuilder instance, params ISqlStatementBuilder[] statements)
            => instance.AddMetadata(statements.Select(x => new MetadataBuilder().WithName(Database.DeleteStoredProcedureStatement).WithValue(x.Build())));

        public static DataObjectInfoBuilder AddDeleteStoredProcedureStatements(this DataObjectInfoBuilder instance, IEnumerable<ISqlStatementBuilder> statements)
            => instance.AddDeleteStoredProcedureStatements(statements.ToArray());

        public static DataObjectInfoBuilder WithCustomAddCommandText(this DataObjectInfoBuilder instance, string? commandText)
            => instance.ReplaceMetadata(Database.AddCustomCommandText, commandText);

        public static DataObjectInfoBuilder WithCustomUpdateCommandText(this DataObjectInfoBuilder instance, string? commandText)
            => instance.ReplaceMetadata(Database.UpdateCustomCommandText, commandText);

        public static DataObjectInfoBuilder WithCustomDeleteCommandText(this DataObjectInfoBuilder instance, string? commandText)
            => instance.ReplaceMetadata(Database.DeleteCustomCommandText, commandText);
    }
}
