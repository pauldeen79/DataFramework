namespace DataFramework.ModelFramework.MetadataNames
{
    internal static class Entities
    {
        /// <summary>
        /// The root name of all metadata names of DataFramework.ModelFramework.Entities.
        /// </summary>
        internal const string EntitiesRootName = Base.RootName + ".Entities";

        internal const string BaseClass = EntitiesRootName + ".BaseClass";
        internal const string PropertyType = EntitiesRootName + ".PropertyType";
        internal const string Interfaces = EntitiesRootName + ".Interfaces";
        internal const string Visibility = EntitiesRootName + ".Visibility";
        internal const string Namespace = EntitiesRootName + ".Namespace";
        internal const string BuildersNamespace = EntitiesRootName + ".Builders.Namespace";
        internal const string IdentitiesNamespace = EntitiesRootName + ".Identities.Namespace";
        internal const string QueriesNamespace = EntitiesRootName + ".Queries.Namespace";
        internal const string QueryBuildersNamespace = EntitiesRootName + ".QueryBuilders.Namespace";
        internal const string DataObjectInfoProviderNamespace = EntitiesRootName + ".DataObjectInfoProvider.Namespace";
        internal const string ComputedTemplate = EntitiesRootName + ".ComputedTemplate";
        internal const string DataObjectInfoAccessor = EntitiesRootName + ".DataObjectInfoAccessor";
        internal const string DataObjectTypeNamePrefix = EntitiesRootName + ".DataObjectTypeNamePrefix";
        internal const string EntitiesAttribute = EntitiesRootName + ".Attribute";
        internal const string EntityBuildersAttribute = EntitiesRootName + ".Builders.Attribute";
        internal const string EntityIdentitiesAttribute = EntitiesRootName + ".Identities.Attribute";
        internal const string EntityExtensionsAttribute = EntitiesRootName + ".Extensions.Attribute";
        internal const string QueriesAttribute = EntitiesRootName + ".Queries.Attribute";
        internal const string QueriesAdditionalInterface = EntitiesRootName + ".Queries.AdditionalInterface";
        internal const string QueriesAdditionalValidFieldName = EntitiesRootName + ".Queries.AdditionalValidFieldName";
        internal const string QueriesCustomMethod = EntitiesRootName + ".Queries.CustomMethod";
        internal const string QueryBuildersAttribute = EntitiesRootName + ".QueryBuilders.Attribute";
        internal const string QueryValidExpressionStatement = EntitiesRootName + ".Queries.ValidExpression.Statements";
        internal const string QueryValidFieldNameStatement = EntitiesRootName + ".Queries.ValidFieldName.Statements";
        internal const string DisallowAdd = EntitiesRootName + ".DisallowAdd";
        internal const string DisallowUpdate = EntitiesRootName + ".DisallowUpdate";
        internal const string DisallowDelete = EntitiesRootName + ".DisallowDelete";

        internal const string GetFieldDefaultOverride = EntitiesRootName + ".GetFieldDefaultOverride";
        internal const string SetFieldDefaultOverride = EntitiesRootName + ".SetFieldDefaultOverride";

        /// <summary>
        /// Marks a property or field as static.
        /// </summary>
        internal const string Static = EntitiesRootName + ".Static";

        /// <summary>
        /// Marks a property or field as virtual.
        /// </summary>
        internal const string Virtual = EntitiesRootName + ".Virtual";

        /// <summary>
        /// Marks a property or field as abstract.
        /// </summary>
        internal const string Abstract = EntitiesRootName + ".Abstract";

        /// <summary>
        /// Marks a property or field as protected.
        /// </summary>
        internal const string Protected = EntitiesRootName + ".Protected";

        /// <summary>
        /// Marks a property or field as override.
        /// </summary>
        internal const string Override = EntitiesRootName + ".Override";

        /// <summary>
        /// Changes the visibility of the backing fields that belong to the property. When not available, the visibility will be internal.
        /// </summary>
        internal const string BackingFieldVisibility = EntitiesRootName + ".BackingFieldVisibility";

        /// <summary>
        /// Changes the default value of the backing fields that belong to the property. When not available, the default value will be default(T).
        /// </summary>
        internal const string BackingFieldDefaultValue = EntitiesRootName + ".BackingFieldDefaultValue";

        /// <summary>
        /// Format string for supplying additional parameters to methods on entities.
        /// </summary>
        internal const string MethodParameterFormatString = EntitiesRootName + ".MethodArguments.{0}";

        /// <summary>
        /// Metadata key for adding SelectFields (of type ModelFramework.Objects.Contracts.IClassField) to generated entities.
        /// </summary>
        internal const string SelectField = EntitiesRootName + ".SelectField";

        /// <summary>
        /// Allows to specify entity class type. (IDataObject, Poco or GenericDataObject)
        /// </summary>
        internal const string EntityClassType = EntitiesRootName + ".EntityClassType";

        /// <summary>
        /// Allows to specify render metadata as attributes type (None, Standard or All)
        /// </summary>
        internal const string RenderMetadataAsAttributesType = EntitiesRootName + ".RenderMetadataAsAttributesType";
    }
}
