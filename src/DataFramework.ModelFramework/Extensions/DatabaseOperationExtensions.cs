using System;
using CrossCutting.Data.Abstractions;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class DatabaseOperationExtensions
    {
        internal static string GetMethodNamePrefix(this DatabaseOperation operation)
            => operation switch
            {
                DatabaseOperation.Insert => "Add",
                DatabaseOperation.Update => "Update",
                DatabaseOperation.Delete => "Delete",
                _ => throw new ArgumentOutOfRangeException(nameof(operation)),
            };
    }
}
