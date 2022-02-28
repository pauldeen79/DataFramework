using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ExpressionFramework.Abstractions.DomainModel;
using QueryFramework.Abstractions;
using QueryFramework.Abstractions.Queries;
using QueryFramework.Core.Queries;

namespace PDC.Net.Core.Queries
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Entities.QueryGenerator", @"1.0.0.0")]
    public partial record ExtraFieldQuery : SingleEntityQuery
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Limit.HasValue && Limit.Value > MaxLimit)
            {
                yield return new ValidationResult("Limit exceeds the maximum of " + MaxLimit, new[] { nameof(Limit), nameof(Limit) });
            }

            foreach (var condition in Conditions)
            {
                if (!IsValidExpression(condition.LeftExpression))
                {
                    yield return new ValidationResult("Invalid field name in left expression: " + condition.LeftExpression, new[] { nameof(Conditions), nameof(Conditions) });
                }
                if (!IsValidExpression(condition.RightExpression))
                {
                    yield return new ValidationResult("Invalid field name in right expression: " + condition.RightExpression, new[] { nameof(Conditions), nameof(Conditions) });
                }
            }
            foreach (var querySortOrder in OrderByFields)
            {
                if (!IsValidExpression(querySortOrder.Field))
                {
                    yield return new ValidationResult("Invalid field name in order by expression: " + querySortOrder.Field, new[] { nameof(OrderByFields), nameof(OrderByFields) });
                }
            }
        }

        private bool IsValidExpression(IExpression expression)
        {
            if (expression is IFieldExpression fieldExpression)
            {
                var result = false;

                // Expression can't be validated here because of support of dynamic extrafields
                //if (expression is PdcCustomQueryExpression) return true;

                return result || ValidFieldNames.Any(s => s.Equals(fieldExpression.FieldName, StringComparison.OrdinalIgnoreCase));
            }

            // You might want to validate the expression to prevent sql injection (unless you can only create query expressions in code)
            return true;
        }

        public ExtraFieldQuery() : this(null, null, Enumerable.Empty<ICondition>(), Enumerable.Empty<IQuerySortOrder>())
        {
        }

        public ExtraFieldQuery(int? limit,
                               int? offset,
                               IEnumerable<ICondition> conditions,
                               IEnumerable<IQuerySortOrder> orderByFields)
            : base(limit, offset, conditions, orderByFields)
        {
        }

        public ExtraFieldQuery(ISingleEntityQuery simpleEntityQuery): this(simpleEntityQuery.Limit,
                                                                           simpleEntityQuery.Offset,
                                                                           simpleEntityQuery.Conditions,
                                                                           simpleEntityQuery.OrderByFields)
        {
        }

        private static readonly string[] ValidFieldNames = new[] { "EntityName", "Description", "FieldNumber", "FieldType" };

        private const int MaxLimit = int.MaxValue;
    }
}

