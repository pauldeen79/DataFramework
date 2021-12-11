using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using QueryFramework.Abstractions;
using QueryFramework.Abstractions.Queries;
using QueryFramework.Core.Queries;

namespace PDC.Net.Core.Queries
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Entities.QueryGenerator", @"1.0.0.0")]
    public partial record CatalogQuery : SingleEntityQuery
    {
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var validationResult in base.Validate(validationContext))
            {
                yield return validationResult;
            }

            if (Limit.HasValue && Limit.Value > MaxLimit)
            {
                yield return new ValidationResult("Limit exceeds the maximum of " + MaxLimit, new[] { nameof(Limit), nameof(Limit) });
            }

            foreach (var condition in Conditions)
            {
                if (!IsValidFieldName(condition.Field))
                {
                    yield return new ValidationResult("Invalid field name in conditions: " + condition.Field.FieldName, new[] { nameof(Conditions), nameof(Conditions) });
                }
                if (!IsValidExpression(condition.Field))
                {
                    yield return new ValidationResult("Invalid expression in conditions: " + condition.Field, new[] { nameof(Conditions), nameof(Conditions) });
                }
            }
            foreach (var querySortOrder in OrderByFields)
            {
                if (!IsValidFieldName(querySortOrder.Field))
                {
                    yield return new ValidationResult("Invalid field name in order by fields: " + querySortOrder.Field.FieldName, new[] { nameof(OrderByFields), nameof(OrderByFields) });
                }
                if (!IsValidExpression(querySortOrder.Field))
                {
                    yield return new ValidationResult("Invalid expression in order by fields: " + querySortOrder.Field, new[] { nameof(OrderByFields), nameof(OrderByFields) });
                }
            }
        }

        private bool IsValidExpression(IQueryExpression expression)
        {
            // You might want to validate the expression to prevent sql injection (unless you can only create query expressions in code)
            return true;
        }

        private bool IsValidFieldName(IQueryExpression expression)
        {
            // default: var result = false;
            // Override because of extrafields transformation
            var result = true;

            // Expression can't be validated here because of support of dynamic extrafields
            //if (expression is PdcCustomQueryExpression) return true;

            return result || ValidFieldNames.Any(s => s.Equals(expression.FieldName, StringComparison.OrdinalIgnoreCase));
        }

        public CatalogQuery() : this(null, null, Enumerable.Empty<IQueryCondition>(), Enumerable.Empty<IQuerySortOrder>())
        {
        }

        public CatalogQuery(int? limit,
                            int? offset,
                            IEnumerable<IQueryCondition> conditions,
                            IEnumerable<IQuerySortOrder> orderByFields)
            : base(limit, offset, conditions, orderByFields)
        {
        }

        public CatalogQuery(ISingleEntityQuery simpleEntityQuery): this(simpleEntityQuery.Limit,
                                                                        simpleEntityQuery.Offset,
                                                                        simpleEntityQuery.Conditions,
                                                                        simpleEntityQuery.OrderByFields)
        {
        }

        private static readonly string[] ValidFieldNames = new[] { "Id", "Name", "DateCreated", "DateLastModified", "DateSynchronized", "DriveSerialNumber", "DriveTypeCodeType", "DriveTypeCode", "DriveTypeDescription", "DriveTotalSize", "DriveFreeSpace", "Recursive", "Sorted", "StartDirectory", "ExtraField1", "ExtraField2", "ExtraField3", "ExtraField4", "ExtraField5", "ExtraField6", "ExtraField7", "ExtraField8", "ExtraField9", "ExtraField10", "ExtraField11", "ExtraField12", "ExtraField13", "ExtraField14", "ExtraField15", "ExtraField16", "IsExistingEntity", "AllFields" };

        private const int MaxLimit = int.MaxValue;
    }
}

