using DataFramework.Abstractions;
using ModelFramework.Common.Extensions;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;

namespace DataFramework.ModelFramework.Extensions
{
    internal static class ClassFieldBuilderExtensions
    {
        internal static ClassFieldBuilder FillFrom(this ClassFieldBuilder instance, IFieldInfo fieldInfo)
            => instance.WithName($"_{fieldInfo.Name.ToPascalCase()}")
                .WithTypeName(fieldInfo.TypeName)
                .WithIsNullable(fieldInfo.IsNullable)
                .WithVisibility(Visibility.Private);
    }
}
