namespace DataFramework.Pipelines.Domains;

/// <summary>
/// Behavior of update and delete concurrency check on UPDATE and DELETE statements. Default is NoFields.
/// </summary>
public enum ConcurrencyCheckBehavior
{
    /// <summary>
    /// Don't use any fields for consistency check. (default)
    /// </summary>
    NoFields = 0,
    /// <summary>
    /// Only use fields that are marked for consistency check.
    /// </summary>
    MarkedFields = 1,
    /// <summary>
    /// Use all fields for consistency check.
    /// </summary>
    AllFields = 2,
}
