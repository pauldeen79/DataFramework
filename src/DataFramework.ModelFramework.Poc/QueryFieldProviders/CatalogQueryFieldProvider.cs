using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using PDC.Net.Core.Entities;
using PDC.Net.Core.QueryExpressions;
using QueryFramework.Abstractions;
using QueryFramework.SqlServer.Abstractions;

namespace DataFramework.ModelFramework.Poc.QueryFieldProviders
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class CatalogQueryFieldProvider : IQueryFieldProvider
    {
        private IEnumerable<ExtraField> ExtraFields { get; }

        public CatalogQueryFieldProvider(IEnumerable<ExtraField> extraFields)
        {
            ExtraFields = extraFields;
        }

        public IEnumerable<string> GetAllFields()
        {
            return GetFieldNames().Select(GetDatabaseFieldName).Where(x => x != null).Cast<string>();
        }

        private IEnumerable<string> GetFieldNames()
        {
            yield return @"Id";
            yield return @"Name";
            yield return @"DateCreated";
            yield return @"DateLastModified";
            yield return @"DateSynchronized";
            yield return @"DriveSerialNumber";
            yield return @"DriveTypeCodeType";
            yield return @"DriveTypeCode";
            yield return @"DriveTypeDescription";
            yield return @"DriveTotalSize";
            yield return @"DriveFreeSpace";
            yield return @"Recursive";
            yield return @"Sorted";
            yield return @"StartDirectory";
            // injected by ExtraField transformation
            yield return @"ExtraField1";
            yield return @"ExtraField2";
            yield return @"ExtraField3";
            yield return @"ExtraField4";
            yield return @"ExtraField5";
            yield return @"ExtraField6";
            yield return @"ExtraField7";
            yield return @"ExtraField8";
            yield return @"ExtraField9";
            yield return @"ExtraField10";
            yield return @"ExtraField11";
            yield return @"ExtraField12";
            yield return @"ExtraField13";
            yield return @"ExtraField14";
            yield return @"ExtraField15";
            yield return @"ExtraField16";
            // injected by computed field
            yield return @"AllFields";
        }

        public string? GetDatabaseFieldName(string queryFieldName)
        {
            // default: return queryFieldName;
            var extraField = ExtraFields.FirstOrDefault(x => x.Name == queryFieldName);
            if (extraField != null)
            {
                return string.Format("ExtraField{0}", extraField.FieldNumber);
            }

            if (queryFieldName == "AllFields")
            {
                return "[Name] + ' ' + [StartDirectory] + ' ' + COALESCE([ExtraField1], '') + ' ' + COALESCE([ExtraField2], '') + ' ' + COALESCE([ExtraField3], '') + ' ' + COALESCE([ExtraField4], '') + ' ' + COALESCE([ExtraField5], '') + ' ' + COALESCE([ExtraField6], '') + ' ' + COALESCE([ExtraField7], '') + ' ' + COALESCE([ExtraField8], '') + ' ' + COALESCE([ExtraField9], '') + ' ' + COALESCE([ExtraField10], '') + ' ' + COALESCE([ExtraField11], '') + ' ' + COALESCE([ExtraField12], '') + ' ' + COALESCE([ExtraField13], '') + ' ' + COALESCE([ExtraField14], '') + ' ' + COALESCE([ExtraField15], '') + ' ' + COALESCE([ExtraField16], '')";
            }

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
