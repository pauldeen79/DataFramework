namespace DataFramework.ModelFramework.MetadataNames
{
    internal static class Shared
    {
        /// <summary>
        /// The root name of all metadata names of DataFramework.ModelFramework.Shared.
        /// </summary>
        internal const string SharedRootName = Base.RootName + ".Shared";

        internal const string DataObjectInfoInstanceName = SharedRootName + ".DataObjectInfoInstance";
        internal const string FieldInfoInstanceName = SharedRootName + ".FieldInfoInstance";
        internal const string PropertyNameDeconflictionFormatStringName = SharedRootName + ".PropertyNameDeconflictionFormatString";
        internal const string OriginalFieldNameName = SharedRootName + ".OriginalField";

        /// <summary>
        /// Custom data object info's (IDataObjectInfo)
        /// </summary>
        internal const string CustomDataObjectInfo = SharedRootName + ".CustomDataObjectInfo";

        /// <summary>
        /// Custom interface or class (ITypeBase)
        /// </summary>
        internal const string CustomClass = SharedRootName + ".CustomClass";
    }
}
