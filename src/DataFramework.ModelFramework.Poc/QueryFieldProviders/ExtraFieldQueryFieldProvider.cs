using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using PDC.Net.Core.QueryExpressions;
using QueryFramework.Abstractions;
using QueryFramework.SqlServer.Abstractions;

namespace DataFramework.ModelFramework.Poc.QueryFieldProviders
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class ExtraFieldQueryFieldProvider : IQueryFieldProvider
    {
        public IEnumerable<string> GetAllFields()
        {
            return GetFieldNames();
        }

        private IEnumerable<string> GetFieldNames()
        {
            yield return "[EntityName]";
            yield return "[Name]";
            yield return "[Description]";
            yield return "[FieldNumber]";
            yield return "[FieldType]";
        }

        public string? GetDatabaseFieldName(string queryFieldName)
        {
            // default: return queryFieldName;
            return queryFieldName;
        }

        public IEnumerable<string> GetSelectFields(IEnumerable<string> querySelectFields)
        {
            // fields which are not mapped need to be excluded
            return querySelectFields;
        }

        public bool ValidateExpression(IQueryExpression expression)
        {
            // default: return true
            return expression is PdcCustomQueryExpression || GetAllFields().Any(s => s.Equals(expression.FieldName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
