﻿using QueryFramework.SqlServer.Abstractions;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    public class ExtraFieldQueryProcessorSettings : IQueryProcessorSettings
    {
        public string TableName => @"[ExtraField]";

        public string Fields => @"[EntityName], [Name], [Description], [FieldNumber], [FieldType]";

        public string DefaultOrderBy => "[Name]";

        public string DefaultWhere => string.Empty;

        public int? OverrideLimit => -1;

        public bool ValidateFieldNames => true;

        public int InitialParameterNumber => 1;
    }
}
