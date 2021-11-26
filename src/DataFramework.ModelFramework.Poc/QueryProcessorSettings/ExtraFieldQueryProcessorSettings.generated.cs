using System.CodeDom.Compiler;
using QueryFramework.SqlServer.Abstractions;

namespace DataFramework.ModelFramework.Poc.QueryProcessorSettings
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class ExtraFieldQueryProcessorSettings : IQueryProcessorSettings
    {
        public string TableName => @"[ExtraField]";

        public string Fields => @"[EntityName], [Name], [Description], [FieldNumber], [FieldType]";

        public string DefaultOrderBy => "[Name]";

        public string DefaultWhere => string.Empty;

        public int? OverridePageSize => null;

        public bool ValidateFieldNames => true;

        public int InitialParameterNumber => 1;
    }
}
