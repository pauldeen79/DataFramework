using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using CrossCutting.Common.Extensions;
using DataFramework.Abstractions;
using DataFramework.ModelFramework.MetadataNames;
using ModelFramework.Common;
using ModelFramework.Objects.Builders;
using ModelFramework.Objects.Contracts;
using QueryFramework.Abstractions;
using QueryFramework.Abstractions.Queries;
using QueryFramework.Core.Queries;

namespace DataFramework.ModelFramework.Extensions
{
    public static partial class DataObjectInfoExtensions
    {
        public static IClass ToQueryClass(this IDataObjectInfo instance, GeneratorSettings settings)
            => instance.ToQueryClassBuilder(settings).Build();

        public static ClassBuilder ToQueryClassBuilder(this IDataObjectInfo instance, GeneratorSettings settings)
        {
            var renderMetadataAsAttributes = instance.GetRenderMetadataAsAttributesType(settings.DefaultRenderMetadataAsAttributes);

            return new ClassBuilder()
                .WithName($"{instance.Name}Query")
                .WithNamespace(instance.GetQueriesNamespace())
                .FillFrom(instance)
                .WithVisibility(instance.Metadata.GetValue(Queries.Visibility, () => instance.IsVisible.ToVisibility()))
                .WithBaseClass(typeof(SingleEntityQuery))
                .AddInterfaces(instance.Metadata
                    .Where(md => md.Name == Queries.Interface)
                    .Select(md => md.Value.ToStringWithNullCheck().FixGenericParameter(instance.GetEntityFullName())))
                .WithRecord()
                .AddFields(GetQueryClassFields(instance))
                .AddMethods(GetQueryClassMethods(instance))
                .AddConstructors(GetQueryClassConstructors())
                .AddAttributes(GetQueryClassAttributes(instance, renderMetadataAsAttributes));
        }

        private static IEnumerable<ClassFieldBuilder> GetQueryClassFields(IDataObjectInfo instance)
        {
            yield return new ClassFieldBuilder()
                .WithName("ValidFieldNames")
                .WithType(typeof(string[]))
                .WithStatic()
                .WithReadOnly()
                .WithDefaultValue(new Literal("new[] { " + string.Join(", ", GetQueryClassValidFieldNames(instance).Select(s => string.Format("\"{0}\"", s))) + " }"));

            yield return new ClassFieldBuilder()
                .WithName("MaxLimit")
                .WithType(typeof(int))
                .WithConstant()
                .WithDefaultValue(new Literal(GetQueryMaxLimit(instance)));
        }

        private static IEnumerable<string> GetQueryClassValidFieldNames(IDataObjectInfo instance)
        {
            foreach (var s in instance.Fields.Select(f => f.Name))
            {
                yield return s;
            }

            foreach (var md in instance.Metadata.Where(x => x.Name == Queries.ValidFieldName))
            {
                var value = md.Value.ToStringWithNullCheck();
                if (!string.IsNullOrEmpty(value))
                {
                    yield return value;
                }
            }
        }

        private static string GetQueryMaxLimit(IDataObjectInfo instance)
        {
            var maxLimit = instance.Metadata.GetValue<int?>(Queries.MaxLimit, () => null);
            return maxLimit == null
                ? "int.MaxValue"
                : maxLimit.Value.ToString(CultureInfo.InvariantCulture);
        }

        private static IEnumerable<ClassMethodBuilder> GetQueryClassMethods(IDataObjectInfo instance)
        {
            var validationResultType = typeof(ValidationResult).FullName;
            yield return new ClassMethodBuilder()
                .WithName(nameof(IValidatableObject.Validate))
                .WithVisibility(Visibility.Public)
                .WithOverride()
                .WithType(typeof(IEnumerable<ValidationResult>))
                .AddParameter("validationContext", typeof(ValidationContext))
                .AddLiteralCodeStatements
                (
                    @"foreach (var validationResult in base.Validate(validationContext))",
                    @"{",
                    @"    yield return validationResult;",
                    @"}",
                    @"if (Limit.HasValue && Limit.Value > MaxLimit)",
                    @"{",
                    $@"    yield return new {validationResultType}(""Limit exceeds the maximum of "" + MaxLimit, new[] {{ nameof(Limit), nameof(Limit) }});",
                    @"}",
                    @"foreach (var condition in Conditions)",
                    @"{",
                    @"    if (!IsValidFieldName(condition.Field))",
                    @"    {",
                    $@"        yield return new {validationResultType}(""Invalid field name in conditions: "" + condition.Field.FieldName, new[] {{ nameof(Conditions), nameof(Conditions) }});",
                    @"    }",
                    @"    if (!IsValidExpression(condition.Field))",
                    @"    {",
                    $@"        yield return new {validationResultType}(""Invalid expression in conditions: "" + condition.Field, new[] {{ nameof(Conditions), nameof(Conditions) }});",
                    @"    }",
                    @"}",
                    @"foreach (var querySortOrder in OrderByFields)",
                    @"{",
                    @"    if (!IsValidFieldName(querySortOrder.Field))",
                    @"    {",
                    $@"        yield return new {validationResultType}(""Invalid field name in order by fields: "" + querySortOrder.Field.FieldName, new[] {{ nameof(OrderByFields), nameof(OrderByFields) }});",
                    @"    }",
                    @"    if (!IsValidExpression(querySortOrder.Field))",
                    @"    {",
                    $@"        yield return new {validationResultType}(""Invalid expression in order by fields: "" + querySortOrder.Field, new[] {{ nameof(OrderByFields), nameof(OrderByFields) }});",
                    @"    }",
                    @"}"
                );

            yield return new ClassMethodBuilder()
                .WithName("IsValidExpression")
                .WithVisibility(Visibility.Private)
                .WithType(typeof(bool))
                .AddCodeStatements(instance.Metadata.GetValues<ICodeStatement>(Queries.ValidExpressionStatement))
                .Chain(x =>
                {
                    if (x.CodeStatements.Count == 0)
                    {
                        x.AddLiteralCodeStatements("return true;");
                    }
                });

            yield return new ClassMethodBuilder()
                .WithName("IsValidFieldName")
                .WithVisibility(Visibility.Private)
                .WithType(typeof(bool))
                .AddCodeStatements(instance.Metadata.GetValues<ICodeStatement>(Queries.ValidFieldNameStatement))
                .Chain(x =>
                {
                    if (x.CodeStatements.Count == 0)
                    {
                        x.AddLiteralCodeStatements("return ValidFieldNames.Any(s => s.Equals(expression.FieldName, StringComparison.OrdinalIgnoreCase));");
                    }
                });
        }

        private static IEnumerable<ClassConstructorBuilder> GetQueryClassConstructors()
        {
            yield return new ClassConstructorBuilder()
                .WithChainCall($"this(null, null, Enumerable.Empty<{typeof(IQueryCondition).FullName}>(), Enumerable.Empty<{typeof(IQuerySortOrder).FullName}>())");

            yield return new ClassConstructorBuilder()
                .AddParameter("limit", typeof(int?))
                .AddParameter("offset", typeof(int?))
                .AddParameter("conditions", typeof(IEnumerable<IQueryCondition>))
                .AddParameter("orderByFields", typeof(IEnumerable<IQuerySortOrder>))
                .WithChainCall("base(limit, offset, conditions, orderByFields)");

            yield return new ClassConstructorBuilder()
                .AddParameter("simpleEntityQuery", typeof(ISingleEntityQuery))
                .WithChainCall("this(simpleEntityQuery.Limit, simpleEntityQuery.Offset, simpleEntityQuery.Conditions, simpleEntityQuery.OrderByFields");
        }

        private static IEnumerable<AttributeBuilder> GetQueryClassAttributes(IDataObjectInfo instance, RenderMetadataAsAttributesTypes renderMetadataAsAttributes)
        {
            yield return new AttributeBuilder().ForCodeGenerator("DataFramework.ModelFramework.Generators.Queries.QueryGenerator");

            if (renderMetadataAsAttributes.HasFlag(RenderMetadataAsAttributesTypes.Custom))
            {
                foreach (var attribute in instance.Metadata.GetValues<IAttribute>(Queries.Attribute))
                {
                    yield return new AttributeBuilder(attribute);
                }
            }
        }
    }
}
