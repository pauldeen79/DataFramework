﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 8.0.10
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#nullable enable
namespace DataFramework.Domain
{
    public partial class DataObjectInfo
    {
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string Name
        {
            get;
        }

        public string? AssemblyName
        {
            get;
        }

        public string? TypeName
        {
            get;
        }

        public string? Description
        {
            get;
        }

        public string? DisplayName
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool IsVisible
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool IsQueryable
        {
            get;
        }

        public bool IsReadOnly
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<DataFramework.Domain.FieldInfo> Fields
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string DatabaseTableName
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string DatabaseSchemaName
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string DatabaseFileGroupName
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string CustomAddDatabaseCommandText
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string CustomUpdateDatabaseCommandText
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string CustomDeleteDatabaseCommandText
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string ViewDefinition
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<DatabaseFramework.Domain.PrimaryKeyConstraint> PrimaryKeyConstraints
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<DatabaseFramework.Domain.ForeignKeyConstraint> ForeignKeyConstraints
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<DatabaseFramework.Domain.Index> Indexes
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<DatabaseFramework.Domain.CheckConstraint> CheckConstraints
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<DataFramework.Domain.EntityMapping> CustomEntityMappings
        {
            get;
        }

        public string? DefaultOrderByFields
        {
            get;
        }

        public string? DefaultWhereClause
        {
            get;
        }

        public System.Nullable<int> OverridePageSize
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public System.Collections.Generic.IReadOnlyCollection<string> AdditionalQueryFields
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.CodeStatementBase> QueryFieldNameStatements
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.CodeStatementBase> QueryExpressionStatements
        {
            get;
        }

        public DataObjectInfo(string name, string? assemblyName, string? typeName, string? description, string? displayName, bool isVisible, bool isQueryable, bool isReadOnly, System.Collections.Generic.IEnumerable<DataFramework.Domain.FieldInfo> fields, string databaseTableName, string databaseSchemaName, string databaseFileGroupName, string customAddDatabaseCommandText, string customUpdateDatabaseCommandText, string customDeleteDatabaseCommandText, string viewDefinition, System.Collections.Generic.IEnumerable<DatabaseFramework.Domain.PrimaryKeyConstraint> primaryKeyConstraints, System.Collections.Generic.IEnumerable<DatabaseFramework.Domain.ForeignKeyConstraint> foreignKeyConstraints, System.Collections.Generic.IEnumerable<DatabaseFramework.Domain.Index> indexes, System.Collections.Generic.IEnumerable<DatabaseFramework.Domain.CheckConstraint> checkConstraints, System.Collections.Generic.IEnumerable<DataFramework.Domain.EntityMapping> customEntityMappings, string? defaultOrderByFields, string? defaultWhereClause, System.Nullable<int> overridePageSize, System.Collections.Generic.IEnumerable<string> additionalQueryFields, System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> queryFieldNameStatements, System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> queryExpressionStatements)
        {
            this.Name = name;
            this.AssemblyName = assemblyName;
            this.TypeName = typeName;
            this.Description = description;
            this.DisplayName = displayName;
            this.IsVisible = isVisible;
            this.IsQueryable = isQueryable;
            this.IsReadOnly = isReadOnly;
            this.Fields = fields is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<DataFramework.Domain.FieldInfo>(fields);
            this.DatabaseTableName = databaseTableName;
            this.DatabaseSchemaName = databaseSchemaName;
            this.DatabaseFileGroupName = databaseFileGroupName;
            this.CustomAddDatabaseCommandText = customAddDatabaseCommandText;
            this.CustomUpdateDatabaseCommandText = customUpdateDatabaseCommandText;
            this.CustomDeleteDatabaseCommandText = customDeleteDatabaseCommandText;
            this.ViewDefinition = viewDefinition;
            this.PrimaryKeyConstraints = primaryKeyConstraints is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<DatabaseFramework.Domain.PrimaryKeyConstraint>(primaryKeyConstraints);
            this.ForeignKeyConstraints = foreignKeyConstraints is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<DatabaseFramework.Domain.ForeignKeyConstraint>(foreignKeyConstraints);
            this.Indexes = indexes is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<DatabaseFramework.Domain.Index>(indexes);
            this.CheckConstraints = checkConstraints is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<DatabaseFramework.Domain.CheckConstraint>(checkConstraints);
            this.CustomEntityMappings = customEntityMappings is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<DataFramework.Domain.EntityMapping>(customEntityMappings);
            this.DefaultOrderByFields = defaultOrderByFields;
            this.DefaultWhereClause = defaultWhereClause;
            this.OverridePageSize = overridePageSize;
            this.AdditionalQueryFields = additionalQueryFields is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<System.String>(additionalQueryFields);
            this.QueryFieldNameStatements = queryFieldNameStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.CodeStatementBase>(queryFieldNameStatements);
            this.QueryExpressionStatements = queryExpressionStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.CodeStatementBase>(queryExpressionStatements);
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public DataFramework.Domain.Builders.DataObjectInfoBuilder ToBuilder()
        {
            return new DataFramework.Domain.Builders.DataObjectInfoBuilder(this);
        }
    }
    public partial class EntityMapping
    {
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string PropertyName
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public object Mapping
        {
            get;
        }

        public EntityMapping(string propertyName, object mapping)
        {
            this.PropertyName = propertyName;
            this.Mapping = mapping;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public DataFramework.Domain.Builders.EntityMappingBuilder ToBuilder()
        {
            return new DataFramework.Domain.Builders.EntityMappingBuilder(this);
        }
    }
    public partial class FieldInfo
    {
        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string Name
        {
            get;
        }

        public string? Description
        {
            get;
        }

        public string? DisplayName
        {
            get;
        }

        public string? TypeName
        {
            get;
        }

        public bool IsNullable
        {
            get;
        }

        public bool IsValueType
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool IsVisible
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool IsPersistable
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool CanGet
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool CanSet
        {
            get;
        }

        public bool IsReadOnly
        {
            get;
        }

        public bool IsIdentityField
        {
            get;
        }

        public bool IsComputed
        {
            get;
        }

        public bool IsRowVersion
        {
            get;
        }

        public bool UseForConcurrencyCheck
        {
            get;
        }

        public object? DefaultValue
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.CodeStatementBase> GetterCodeStatements
        {
            get;
        }

        public System.Nullable<int> StringMaxLength
        {
            get;
        }

        public System.Nullable<bool> IsMaxLengthString
        {
            get;
        }

        public string? DatabaseStringCollation
        {
            get;
        }

        public System.Nullable<byte> DatabaseNumericPrecision
        {
            get;
        }

        public System.Nullable<byte> DatabaseNumericScale
        {
            get;
        }

        public bool SkipFieldOnFind
        {
            get;
        }

        public string? DatabaseFieldName
        {
            get;
        }

        public string? DatabaseFieldType
        {
            get;
        }

        public string? DatabaseReaderMethodName
        {
            get;
        }

        public string? DatabaseCheckConstraintExpression
        {
            get;
        }

        public System.Nullable<bool> OverrideUseOnInsert
        {
            get;
        }

        public System.Nullable<bool> OverrideUseOnUpdate
        {
            get;
        }

        public System.Nullable<bool> OverrideUseOnDelete
        {
            get;
        }

        public System.Nullable<bool> OverrideUseOnSelect
        {
            get;
        }

        public System.Nullable<bool> IsRequiredInDatabase
        {
            get;
        }

        public bool IsDatabaseIdentityField
        {
            get;
        }

        public FieldInfo(string name, string? description, string? displayName, string? typeName, bool isNullable, bool isValueType, bool isVisible, bool isPersistable, bool canGet, bool canSet, bool isReadOnly, bool isIdentityField, bool isComputed, bool isRowVersion, bool useForConcurrencyCheck, object? defaultValue, System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> getterCodeStatements, System.Nullable<int> stringMaxLength, System.Nullable<bool> isMaxLengthString, string? databaseStringCollation, System.Nullable<byte> databaseNumericPrecision, System.Nullable<byte> databaseNumericScale, bool skipFieldOnFind, string? databaseFieldName, string? databaseFieldType, string? databaseReaderMethodName, string? databaseCheckConstraintExpression, System.Nullable<bool> overrideUseOnInsert, System.Nullable<bool> overrideUseOnUpdate, System.Nullable<bool> overrideUseOnDelete, System.Nullable<bool> overrideUseOnSelect, System.Nullable<bool> isRequiredInDatabase, bool isDatabaseIdentityField)
        {
            this.Name = name;
            this.Description = description;
            this.DisplayName = displayName;
            this.TypeName = typeName;
            this.IsNullable = isNullable;
            this.IsValueType = isValueType;
            this.IsVisible = isVisible;
            this.IsPersistable = isPersistable;
            this.CanGet = canGet;
            this.CanSet = canSet;
            this.IsReadOnly = isReadOnly;
            this.IsIdentityField = isIdentityField;
            this.IsComputed = isComputed;
            this.IsRowVersion = isRowVersion;
            this.UseForConcurrencyCheck = useForConcurrencyCheck;
            this.DefaultValue = defaultValue;
            this.GetterCodeStatements = getterCodeStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.CodeStatementBase>(getterCodeStatements);
            this.StringMaxLength = stringMaxLength;
            this.IsMaxLengthString = isMaxLengthString;
            this.DatabaseStringCollation = databaseStringCollation;
            this.DatabaseNumericPrecision = databaseNumericPrecision;
            this.DatabaseNumericScale = databaseNumericScale;
            this.SkipFieldOnFind = skipFieldOnFind;
            this.DatabaseFieldName = databaseFieldName;
            this.DatabaseFieldType = databaseFieldType;
            this.DatabaseReaderMethodName = databaseReaderMethodName;
            this.DatabaseCheckConstraintExpression = databaseCheckConstraintExpression;
            this.OverrideUseOnInsert = overrideUseOnInsert;
            this.OverrideUseOnUpdate = overrideUseOnUpdate;
            this.OverrideUseOnDelete = overrideUseOnDelete;
            this.OverrideUseOnSelect = overrideUseOnSelect;
            this.IsRequiredInDatabase = isRequiredInDatabase;
            this.IsDatabaseIdentityField = isDatabaseIdentityField;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public DataFramework.Domain.Builders.FieldInfoBuilder ToBuilder()
        {
            return new DataFramework.Domain.Builders.FieldInfoBuilder(this);
        }
    }
}
#nullable disable
