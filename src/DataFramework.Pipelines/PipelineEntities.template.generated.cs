﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 9.0.3
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
#nullable enable
namespace DataFramework.Pipelines
{
    public partial class PipelineSettings
    {
        public bool EnableNullableContext
        {
            get;
        }

        public DataFramework.Pipelines.Domains.ConcurrencyCheckBehavior ConcurrencyCheckBehavior
        {
            get;
        }

        public DataFramework.Pipelines.Domains.EntityClassType EntityClassType
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string DefaultEntityNamespace
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string DefaultIdentityNamespace
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string DefaultBuilderNamespace
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool AddComponentModelAttributes
        {
            get;
        }

        public ClassFramework.Domain.Domains.Visibility CommandEntityProviderVisibility
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string CommandEntityProviderNamespace
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool CommandProviderEnableAdd
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool CommandProviderEnableUpdate
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(true)]
        public bool CommandProviderEnableDelete
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.CodeStatementBase> CommandEntityProviderAddResultEntityStatements
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.CodeStatementBase> CommandEntityProviderAddAfterReadStatements
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.CodeStatementBase> CommandEntityProviderUpdateResultEntityStatements
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.CodeStatementBase> CommandEntityProviderUpdateAfterReadStatements
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.CodeStatementBase> CommandEntityProviderDeleteResultEntityStatements
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.CodeStatementBase> CommandEntityProviderDeleteAfterReadStatements
        {
            get;
        }

        public ClassFramework.Domain.Domains.Visibility CommandProviderVisibility
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string CommandProviderNamespace
        {
            get;
        }

        public bool UseAddStoredProcedure
        {
            get;
        }

        public bool UseUpdateStoredProcedure
        {
            get;
        }

        public bool UseDeleteStoredProcedure
        {
            get;
        }

        public ClassFramework.Domain.Domains.Visibility DatabaseEntityRetrieverProviderVisibility
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string DatabaseEntityRetrieverProviderNamespace
        {
            get;
        }

        public ClassFramework.Domain.Domains.Visibility DatabaseEntityRetrieverSettingsProviderVisibility
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string DatabaseEntityRetrieverSettingsProviderNamespace
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string AddStoredProcedureName
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string UpdateStoredProcedureName
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        public string DeleteStoredProcedureName
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<DatabaseFramework.Domain.SqlStatementBase> AddStoredProcedureStatements
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<DatabaseFramework.Domain.SqlStatementBase> UpdateStoredProcedureStatements
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<DatabaseFramework.Domain.SqlStatementBase> DeleteStoredProcedureStatements
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(CrossCutting.Data.Abstractions.DatabaseOperation.Insert)]
        public CrossCutting.Data.Abstractions.DatabaseOperation DatabaseCommandTypeForInsertText
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(CrossCutting.Data.Abstractions.DatabaseOperation.Insert)]
        public CrossCutting.Data.Abstractions.DatabaseOperation DatabaseCommandTypeForInsertParameters
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(CrossCutting.Data.Abstractions.DatabaseOperation.Update)]
        public CrossCutting.Data.Abstractions.DatabaseOperation DatabaseCommandTypeForUpdateText
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(CrossCutting.Data.Abstractions.DatabaseOperation.Update)]
        public CrossCutting.Data.Abstractions.DatabaseOperation DatabaseCommandTypeForUpdateParameters
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(CrossCutting.Data.Abstractions.DatabaseOperation.Delete)]
        public CrossCutting.Data.Abstractions.DatabaseOperation DatabaseCommandTypeForDeleteText
        {
            get;
        }

        [System.ComponentModel.DefaultValueAttribute(CrossCutting.Data.Abstractions.DatabaseOperation.Delete)]
        public CrossCutting.Data.Abstractions.DatabaseOperation DatabaseCommandTypeForDeleteParameters
        {
            get;
        }

        public ClassFramework.Domain.Domains.Visibility EntityMapperVisibility
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string EntityMapperNamespace
        {
            get;
        }

        public ClassFramework.Domain.Domains.Visibility IdentityCommandProviderVisibility
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string IdentityCommandProviderNamespace
        {
            get;
        }

        public ClassFramework.Domain.Domains.Visibility DatabasePagedEntityRetrieverSettingsVisibility
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string DatabasePagedEntityRetrieverSettingsNamespace
        {
            get;
        }

        public ClassFramework.Domain.Domains.Visibility QueryVisibility
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string QueryNamespace
        {
            get;
        }

        public System.Nullable<int> QueryMaxLimit
        {
            get;
        }

        public bool CreateQueryAsRecord
        {
            get;
        }

        public ClassFramework.Domain.Domains.Visibility QueryBuilderVisibility
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string QueryBuilderNamespace
        {
            get;
        }

        public ClassFramework.Domain.Domains.Visibility QueryFieldInfoVisibility
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string QueryFieldInfoNamespace
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.Field> QueryFieldInfoFields
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.Parameter> QueryFieldInfoConstructorParameters
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.CodeStatementBase> QueryFieldInfoConstructorCodeStatements
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.CodeStatementBase> QueryFieldInfoGetAllFieldsCodeStatements
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [CrossCutting.Common.DataAnnotations.ValidateObjectAttribute]
        public System.Collections.Generic.IReadOnlyCollection<ClassFramework.Domain.CodeStatementBase> QueryFieldInfoGetDatabaseFieldNameCodeStatements
        {
            get;
        }

        public ClassFramework.Domain.Domains.Visibility QueryFieldInfoProviderVisibility
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string QueryFieldInfoProviderNamespace
        {
            get;
        }

        public ClassFramework.Domain.Domains.Visibility RepositoryVisibility
        {
            get;
        }

        public ClassFramework.Domain.Domains.Visibility RepositoryInterfaceVisibility
        {
            get;
        }

        public bool UseRepositoryInterface
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string RepositoryNamespace
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string RepositoryInterfaceNamespace
        {
            get;
        }

        public ClassFramework.Domain.Domains.Visibility DependencyInjectionVisibility
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute(AllowEmptyStrings = true)]
        public string DependencyInjectionNamespace
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [System.ComponentModel.DefaultValueAttribute(@"ServiceCollectionExtensions")]
        public string DependencyInjectionClassName
        {
            get;
        }

        [System.ComponentModel.DataAnnotations.RequiredAttribute]
        [System.ComponentModel.DefaultValueAttribute(@"Add{{EntityName}}Dependencies")]
        public string DependencyInjectionMethodName
        {
            get;
        }

        public PipelineSettings(bool enableNullableContext, DataFramework.Pipelines.Domains.ConcurrencyCheckBehavior concurrencyCheckBehavior, DataFramework.Pipelines.Domains.EntityClassType entityClassType, string defaultEntityNamespace, string defaultIdentityNamespace, string defaultBuilderNamespace, bool addComponentModelAttributes, ClassFramework.Domain.Domains.Visibility commandEntityProviderVisibility, string commandEntityProviderNamespace, bool commandProviderEnableAdd, bool commandProviderEnableUpdate, bool commandProviderEnableDelete, System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> commandEntityProviderAddResultEntityStatements, System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> commandEntityProviderAddAfterReadStatements, System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> commandEntityProviderUpdateResultEntityStatements, System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> commandEntityProviderUpdateAfterReadStatements, System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> commandEntityProviderDeleteResultEntityStatements, System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> commandEntityProviderDeleteAfterReadStatements, ClassFramework.Domain.Domains.Visibility commandProviderVisibility, string commandProviderNamespace, bool useAddStoredProcedure, bool useUpdateStoredProcedure, bool useDeleteStoredProcedure, ClassFramework.Domain.Domains.Visibility databaseEntityRetrieverProviderVisibility, string databaseEntityRetrieverProviderNamespace, ClassFramework.Domain.Domains.Visibility databaseEntityRetrieverSettingsProviderVisibility, string databaseEntityRetrieverSettingsProviderNamespace, string addStoredProcedureName, string updateStoredProcedureName, string deleteStoredProcedureName, System.Collections.Generic.IEnumerable<DatabaseFramework.Domain.SqlStatementBase> addStoredProcedureStatements, System.Collections.Generic.IEnumerable<DatabaseFramework.Domain.SqlStatementBase> updateStoredProcedureStatements, System.Collections.Generic.IEnumerable<DatabaseFramework.Domain.SqlStatementBase> deleteStoredProcedureStatements, CrossCutting.Data.Abstractions.DatabaseOperation databaseCommandTypeForInsertText, CrossCutting.Data.Abstractions.DatabaseOperation databaseCommandTypeForInsertParameters, CrossCutting.Data.Abstractions.DatabaseOperation databaseCommandTypeForUpdateText, CrossCutting.Data.Abstractions.DatabaseOperation databaseCommandTypeForUpdateParameters, CrossCutting.Data.Abstractions.DatabaseOperation databaseCommandTypeForDeleteText, CrossCutting.Data.Abstractions.DatabaseOperation databaseCommandTypeForDeleteParameters, ClassFramework.Domain.Domains.Visibility entityMapperVisibility, string entityMapperNamespace, ClassFramework.Domain.Domains.Visibility identityCommandProviderVisibility, string identityCommandProviderNamespace, ClassFramework.Domain.Domains.Visibility databasePagedEntityRetrieverSettingsVisibility, string databasePagedEntityRetrieverSettingsNamespace, ClassFramework.Domain.Domains.Visibility queryVisibility, string queryNamespace, System.Nullable<int> queryMaxLimit, bool createQueryAsRecord, ClassFramework.Domain.Domains.Visibility queryBuilderVisibility, string queryBuilderNamespace, ClassFramework.Domain.Domains.Visibility queryFieldInfoVisibility, string queryFieldInfoNamespace, System.Collections.Generic.IEnumerable<ClassFramework.Domain.Field> queryFieldInfoFields, System.Collections.Generic.IEnumerable<ClassFramework.Domain.Parameter> queryFieldInfoConstructorParameters, System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> queryFieldInfoConstructorCodeStatements, System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> queryFieldInfoGetAllFieldsCodeStatements, System.Collections.Generic.IEnumerable<ClassFramework.Domain.CodeStatementBase> queryFieldInfoGetDatabaseFieldNameCodeStatements, ClassFramework.Domain.Domains.Visibility queryFieldInfoProviderVisibility, string queryFieldInfoProviderNamespace, ClassFramework.Domain.Domains.Visibility repositoryVisibility, ClassFramework.Domain.Domains.Visibility repositoryInterfaceVisibility, bool useRepositoryInterface, string repositoryNamespace, string repositoryInterfaceNamespace, ClassFramework.Domain.Domains.Visibility dependencyInjectionVisibility, string dependencyInjectionNamespace, string dependencyInjectionClassName, string dependencyInjectionMethodName)
        {
            this.EnableNullableContext = enableNullableContext;
            this.ConcurrencyCheckBehavior = concurrencyCheckBehavior;
            this.EntityClassType = entityClassType;
            this.DefaultEntityNamespace = defaultEntityNamespace;
            this.DefaultIdentityNamespace = defaultIdentityNamespace;
            this.DefaultBuilderNamespace = defaultBuilderNamespace;
            this.AddComponentModelAttributes = addComponentModelAttributes;
            this.CommandEntityProviderVisibility = commandEntityProviderVisibility;
            this.CommandEntityProviderNamespace = commandEntityProviderNamespace;
            this.CommandProviderEnableAdd = commandProviderEnableAdd;
            this.CommandProviderEnableUpdate = commandProviderEnableUpdate;
            this.CommandProviderEnableDelete = commandProviderEnableDelete;
            this.CommandEntityProviderAddResultEntityStatements = commandEntityProviderAddResultEntityStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.CodeStatementBase>(commandEntityProviderAddResultEntityStatements);
            this.CommandEntityProviderAddAfterReadStatements = commandEntityProviderAddAfterReadStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.CodeStatementBase>(commandEntityProviderAddAfterReadStatements);
            this.CommandEntityProviderUpdateResultEntityStatements = commandEntityProviderUpdateResultEntityStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.CodeStatementBase>(commandEntityProviderUpdateResultEntityStatements);
            this.CommandEntityProviderUpdateAfterReadStatements = commandEntityProviderUpdateAfterReadStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.CodeStatementBase>(commandEntityProviderUpdateAfterReadStatements);
            this.CommandEntityProviderDeleteResultEntityStatements = commandEntityProviderDeleteResultEntityStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.CodeStatementBase>(commandEntityProviderDeleteResultEntityStatements);
            this.CommandEntityProviderDeleteAfterReadStatements = commandEntityProviderDeleteAfterReadStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.CodeStatementBase>(commandEntityProviderDeleteAfterReadStatements);
            this.CommandProviderVisibility = commandProviderVisibility;
            this.CommandProviderNamespace = commandProviderNamespace;
            this.UseAddStoredProcedure = useAddStoredProcedure;
            this.UseUpdateStoredProcedure = useUpdateStoredProcedure;
            this.UseDeleteStoredProcedure = useDeleteStoredProcedure;
            this.DatabaseEntityRetrieverProviderVisibility = databaseEntityRetrieverProviderVisibility;
            this.DatabaseEntityRetrieverProviderNamespace = databaseEntityRetrieverProviderNamespace;
            this.DatabaseEntityRetrieverSettingsProviderVisibility = databaseEntityRetrieverSettingsProviderVisibility;
            this.DatabaseEntityRetrieverSettingsProviderNamespace = databaseEntityRetrieverSettingsProviderNamespace;
            this.AddStoredProcedureName = addStoredProcedureName;
            this.UpdateStoredProcedureName = updateStoredProcedureName;
            this.DeleteStoredProcedureName = deleteStoredProcedureName;
            this.AddStoredProcedureStatements = addStoredProcedureStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<DatabaseFramework.Domain.SqlStatementBase>(addStoredProcedureStatements);
            this.UpdateStoredProcedureStatements = updateStoredProcedureStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<DatabaseFramework.Domain.SqlStatementBase>(updateStoredProcedureStatements);
            this.DeleteStoredProcedureStatements = deleteStoredProcedureStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<DatabaseFramework.Domain.SqlStatementBase>(deleteStoredProcedureStatements);
            this.DatabaseCommandTypeForInsertText = databaseCommandTypeForInsertText;
            this.DatabaseCommandTypeForInsertParameters = databaseCommandTypeForInsertParameters;
            this.DatabaseCommandTypeForUpdateText = databaseCommandTypeForUpdateText;
            this.DatabaseCommandTypeForUpdateParameters = databaseCommandTypeForUpdateParameters;
            this.DatabaseCommandTypeForDeleteText = databaseCommandTypeForDeleteText;
            this.DatabaseCommandTypeForDeleteParameters = databaseCommandTypeForDeleteParameters;
            this.EntityMapperVisibility = entityMapperVisibility;
            this.EntityMapperNamespace = entityMapperNamespace;
            this.IdentityCommandProviderVisibility = identityCommandProviderVisibility;
            this.IdentityCommandProviderNamespace = identityCommandProviderNamespace;
            this.DatabasePagedEntityRetrieverSettingsVisibility = databasePagedEntityRetrieverSettingsVisibility;
            this.DatabasePagedEntityRetrieverSettingsNamespace = databasePagedEntityRetrieverSettingsNamespace;
            this.QueryVisibility = queryVisibility;
            this.QueryNamespace = queryNamespace;
            this.QueryMaxLimit = queryMaxLimit;
            this.CreateQueryAsRecord = createQueryAsRecord;
            this.QueryBuilderVisibility = queryBuilderVisibility;
            this.QueryBuilderNamespace = queryBuilderNamespace;
            this.QueryFieldInfoVisibility = queryFieldInfoVisibility;
            this.QueryFieldInfoNamespace = queryFieldInfoNamespace;
            this.QueryFieldInfoFields = queryFieldInfoFields is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.Field>(queryFieldInfoFields);
            this.QueryFieldInfoConstructorParameters = queryFieldInfoConstructorParameters is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.Parameter>(queryFieldInfoConstructorParameters);
            this.QueryFieldInfoConstructorCodeStatements = queryFieldInfoConstructorCodeStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.CodeStatementBase>(queryFieldInfoConstructorCodeStatements);
            this.QueryFieldInfoGetAllFieldsCodeStatements = queryFieldInfoGetAllFieldsCodeStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.CodeStatementBase>(queryFieldInfoGetAllFieldsCodeStatements);
            this.QueryFieldInfoGetDatabaseFieldNameCodeStatements = queryFieldInfoGetDatabaseFieldNameCodeStatements is null ? null! : new CrossCutting.Common.ReadOnlyValueCollection<ClassFramework.Domain.CodeStatementBase>(queryFieldInfoGetDatabaseFieldNameCodeStatements);
            this.QueryFieldInfoProviderVisibility = queryFieldInfoProviderVisibility;
            this.QueryFieldInfoProviderNamespace = queryFieldInfoProviderNamespace;
            this.RepositoryVisibility = repositoryVisibility;
            this.RepositoryInterfaceVisibility = repositoryInterfaceVisibility;
            this.UseRepositoryInterface = useRepositoryInterface;
            this.RepositoryNamespace = repositoryNamespace;
            this.RepositoryInterfaceNamespace = repositoryInterfaceNamespace;
            this.DependencyInjectionVisibility = dependencyInjectionVisibility;
            this.DependencyInjectionNamespace = dependencyInjectionNamespace;
            this.DependencyInjectionClassName = dependencyInjectionClassName;
            this.DependencyInjectionMethodName = dependencyInjectionMethodName;
            System.ComponentModel.DataAnnotations.Validator.ValidateObject(this, new System.ComponentModel.DataAnnotations.ValidationContext(this, null, null), true);
        }

        public DataFramework.Pipelines.Builders.PipelineSettingsBuilder ToBuilder()
        {
            return new DataFramework.Pipelines.Builders.PipelineSettingsBuilder(this);
        }
    }
}
#nullable disable
