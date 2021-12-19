﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using QueryFramework.Abstractions;
using QueryFramework.SqlServer.Abstractions;

namespace DataFramework.ModelFramework.Poc.QueryFieldProviders
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class ExtraFieldQueryFieldProvider : IQueryFieldProvider
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

        public IEnumerable<string> GetSelectFields(IEnumerable<string> querySelectFields)
        {
            // fields which are not mapped need to be excluded
            return querySelectFields;
        }

        public bool ValidateExpression(IQueryExpression expression)
        {
            // default: return true
            return true;
        }
    }
}
