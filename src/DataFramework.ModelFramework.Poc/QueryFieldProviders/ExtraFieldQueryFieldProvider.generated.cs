using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using PDC.Net.Core.Queries;
using QueryFramework.Abstractions;
using QueryFramework.SqlServer.Abstractions;

namespace DataFramework.ModelFramework.Poc.QueryFieldProviders
{
#nullable enable
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class ExtraFieldQueryFieldInfoProvider : IQueryFieldInfoProvider
    {
        public bool TryCreate(IQuery query, out IQueryFieldInfo? result)
        {
            if (query is ExtraFieldQuery)
            {
                result = new ExtraFieldQueryFieldInfo();
                return true;
            }

            result = default;
            return false;
        }
    }
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class ExtraFieldQueryFieldInfo : IQueryFieldInfo
    {
        public IEnumerable<string> GetAllFields()
        {
            yield return "[EntityName]";
            yield return "[Name]";
            yield return "[Description]";
            yield return "[FieldNumber]";
            yield return "[FieldType]";
        }

        public string? GetDatabaseFieldName(string queryFieldName)
        {
            // default: return GetAllFields().FirstOrDefault(x => x.Equals(queryFieldName, StringComparison.OrdinalIgnoreCase));
            return GetAllFields().FirstOrDefault(x => x.Equals(queryFieldName, StringComparison.OrdinalIgnoreCase));
        }
    }
#nullable restore
}
