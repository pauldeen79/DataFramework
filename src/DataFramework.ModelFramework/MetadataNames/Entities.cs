namespace DataFramework.ModelFramework.MetadataNames
{
    public static class Entities
    {
        /// <summary>
        /// The root name of all metadata names of DataFramework.ModelFramework.Entities.
        /// </summary>
        public const string EntitiesRootName = Base.RootName + ".Entities";

        public const string BaseClass = EntitiesRootName + ".BaseClass";
        public const string PropertyType = EntitiesRootName + ".PropertyType";
        public const string Interfaces = EntitiesRootName + ".Interfaces";
        public const string Visibility = EntitiesRootName + ".Visibility";
        public const string Namespace = EntitiesRootName + ".Namespace";
        public const string BuildersNamespace = EntitiesRootName + ".Builders.Namespace";
        public const string IdentitiesNamespace = EntitiesRootName + ".Identities.Namespace";
        public const string QueriesNamespace = EntitiesRootName + ".Queries.Namespace";
        public const string QueryBuildersNamespace = EntitiesRootName + ".QueryBuilders.Namespace";
        public const string DataObjectInfoProviderNamespace = EntitiesRootName + ".DataObjectInfoProvider.Namespace";
        public const string ComputedTemplate = EntitiesRootName + ".ComputedTemplate";
        public const string DataObjectInfoAccessor = EntitiesRootName + ".DataObjectInfoAccessor";
        public const string DataObjectTypeNamePrefix = EntitiesRootName + ".DataObjectTypeNamePrefix";
        public const string EntitiesAttribute = EntitiesRootName + ".Attribute";
        public const string EntityBuildersAttribute = EntitiesRootName + ".Builders.Attribute";
        public const string EntityIdentitiesAttribute = EntitiesRootName + ".Identities.Attribute";
        public const string EntityExtensionsAttribute = EntitiesRootName + ".Extensions.Attribute";
        public const string QueriesAttribute = EntitiesRootName + ".Queries.Attribute";
        public const string QueriesAdditionalInterface = EntitiesRootName + ".Queries.AdditionalInterface";
        public const string QueriesAdditionalValidFieldName = EntitiesRootName + ".Queries.AdditionalValidFieldName";
        public const string QueriesCustomMethod = EntitiesRootName + ".Queries.CustomMethod";
        public const string QueryBuildersAttribute = EntitiesRootName + ".QueryBuilders.Attribute";
        public const string QueryValidExpressionStatement = EntitiesRootName + ".Queries.ValidExpression.Statements";
        public const string QueryValidFieldNameStatement = EntitiesRootName + ".Queries.ValidFieldName.Statements";
        public const string DisallowAdd = EntitiesRootName + ".DisallowAdd";
        public const string DisallowUpdate = EntitiesRootName + ".DisallowUpdate";
        public const string DisallowDelete = EntitiesRootName + ".DisallowDelete";

        /// <summary>
        /// Arguments for the call to the CreateDataObjectInfo method from the GetDataObjectInfo method.
        /// </summary>
        public const string GetDataObjectInfoCallArguments = EntitiesRootName + ".GetDataObjectInfoCallArguments";

        /// <summary>
        /// Header code for the GetDataObjectInfo method.
        /// </summary>
        public const string GetDataObjectInfoHeaderTemplateName = EntitiesRootName + ".GetDataObjectInfoHeaderTemplate";

        /// <summary>
        /// Footer code for the GetDataObjectInfo method.
        /// </summary>
        public const string GetDataObjectInfoFooterTemplateName = EntitiesRootName + ".GetDataObjectInfoFooterTemplate";

        /// <summary>
        /// Arguments for the call to the GetDataObjectInfo extension method from the CreateDataObjectInfo method.
        /// </summary>
        public const string CreateDataObjectInfoCallArguments = EntitiesRootName + ".CreateDataObjectInfoCallArguments";

        public const string GetFieldDefaultOverride = EntitiesRootName + ".GetFieldDefaultOverride";
        public const string SetFieldDefaultOverride = EntitiesRootName + ".SetFieldDefaultOverride";

        /// <summary>
        /// Marks a property or field as static.
        /// </summary>
        public const string Static = EntitiesRootName + ".Static";

        /// <summary>
        /// Marks a property or field as virtual.
        /// </summary>
        public const string Virtual = EntitiesRootName + ".Virtual";

        /// <summary>
        /// Marks a property or field as abstract.
        /// </summary>
        public const string Abstract = EntitiesRootName + ".Abstract";

        /// <summary>
        /// Marks a property or field as protected.
        /// </summary>
        public const string Protected = EntitiesRootName + ".Protected";

        /// <summary>
        /// Marks a property or field as override.
        /// </summary>
        public const string Override = EntitiesRootName + ".Override";

        /// <summary>
        /// Changes the visibility of the backing fields that belong to the property. When not available, the visibility will be internal.
        /// </summary>
        public const string BackingFieldVisibility = EntitiesRootName + ".BackingFieldVisibility";

        /// <summary>
        /// Changes the default value of the backing fields that belong to the property. When not available, the default value will be default(T).
        /// </summary>
        public const string BackingFieldDefaultValue = EntitiesRootName + ".BackingFieldDefaultValue";

        public const string MethodBodyHeaderTemplateName = EntitiesRootName + ".MethodBodyHeaderTemplateName";
        public const string MethodBodyItemTemplateName = EntitiesRootName + ".MethodBodyItemTemplateName";
        public const string MethodBodyFooterTemplateName = EntitiesRootName + ".MethodBodyFooterTemplateName";

        /// <summary>
        /// Format string for supplying additional parameters to methods on entities.
        /// </summary>
        public const string MethodParameterFormatString = EntitiesRootName + ".MethodArguments.{0}";

        /// <summary>
        /// Metadata key for adding SelectFields (of type ModelFramework.Objects.Contracts.IClassField) to generated entities.
        /// </summary>
        public const string SelectField = EntitiesRootName + ".SelectField";

        /// <summary>
        /// Allows to specify entity class type. (IDataObject, Poco or GenericDataObject)
        /// </summary>
        public const string EntityClassType = EntitiesRootName + ".EntityClassType";

        /// <summary>
        /// Allows to specify render metadata as attributes type (None, Standard or All)
        /// </summary>
        public const string RenderMetadataAsAttributesType = EntitiesRootName + ".RenderMetadataAsAttributesType";

        public const string SourcePropertyValueName = EntitiesRootName + ".SourcePropertyValue";
        public const string MapFromRepositoryDelegateValueName = EntitiesRootName + ".MapFromRepositoryDelegateValue";
        public const string MapToRepositoryDelegateValueName = EntitiesRootName + ".MapToRepositoryDelegateValue";
    }
}
