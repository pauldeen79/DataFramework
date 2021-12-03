namespace DataFramework.ModelFramework
{
    /// <summary>
    /// Behavior of update and delete concurrency check on UPDATE and DELETE statements. Default is MarkedFields.
    /// </summary>
    public enum ConcurrencyCheckBehavior
    {
        /// <summary>
        /// Only use fields that are marked for consistency check. (default)
        /// </summary>
        MarkedFields = 0,
        /// <summary>
        /// Don't use any fields for consistency check.
        /// </summary>
        NoFields = 1,
        /// <summary>
        /// Use all fields for consistency check.
        /// </summary>
        AllFields = 2,
    }
}
