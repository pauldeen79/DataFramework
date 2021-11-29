using System;
using System.Collections.Generic;
using System.Text;

namespace DataFramework.ModelFramework.MetadataNames
{
    public static class Shared
    {
        /// <summary>
        /// The root name of all metadata names of DataFramework.ModelFramework.Shared.
        /// </summary>
        public const string SharedRootName = Base.RootName + ".Shared";

        public const string DataObjectInfoInstanceName = SharedRootName + ".DataObjectInfoInstance";
        public const string FieldInfoInstanceName = SharedRootName + ".FieldInfoInstance";
        public const string PropertyNameDeconflictionFormatStringName = SharedRootName + ".PropertyNameDeconflictionFormatString";
        public const string OriginalFieldNameName = SharedRootName + ".OriginalField";

        /// <summary>
        /// Custom data object info's (IDataObjectInfo)
        /// </summary>
        public const string CustomDataObjectInfoName = SharedRootName + ".CustomDataObjectInfo";

        /// <summary>
        /// Custom interface or class (ITypeBase)
        /// </summary>
        public const string CustomClass = SharedRootName + ".CustomClass";
    }
}
