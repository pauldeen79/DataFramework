﻿using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ExpressionFramework.Domain;
using ExpressionFramework.Domain.Evaluatables;
using ExpressionFramework.Domain.Expressions;
using QueryFramework.Abstractions;
using QueryFramework.Abstractions.Builders;
using QueryFramework.Abstractions.Extensions;
using QueryFramework.Core;

namespace PDC.Net.Core.Queries
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Entities.QueryGenerator", @"1.0.0.0")]
    public partial record CatalogQuery : Query, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Limit.HasValue && Limit.Value > MaxLimit)
            {
                yield return new ValidationResult("Limit exceeds the maximum of " + MaxLimit, new[] { nameof(Limit), nameof(Limit) });
            }

            foreach (var condition in Filter.Conditions)
            {
                if (!IsValidExpression(condition.LeftExpression))
                {
                    yield return new ValidationResult("Invalid field name in left expression: " + condition.LeftExpression, new[] { nameof(Filter), nameof(Filter) });
                }
                if (!IsValidExpression(condition.RightExpression))
                {
                    yield return new ValidationResult("Invalid field name in right expression: " + condition.RightExpression, new[] { nameof(Filter), nameof(Filter) });
                }
            }
            foreach (var querySortOrder in OrderByFields)
            {
                if (!IsValidExpression(querySortOrder.FieldNameExpression))
                {
                    yield return new ValidationResult("Invalid field name in order by expression: " + querySortOrder.FieldNameExpression, new[] { nameof(OrderByFields), nameof(OrderByFields) });
                }
            }
        }

        private bool IsValidExpression(Expression expression)
        {
            if (expression is FieldExpression fieldExpression)
            {
                // default: var result = false;
                // Override because of extrafields transformation
                var result = true;

                // Expression can't be validated here because of support of dynamic extrafields
                //if (expression is PdcCustomQueryExpression) return true;

                return result || ValidFieldNames.Any(s => s.Equals(fieldExpression.GetFieldName(), StringComparison.OrdinalIgnoreCase));
            }

            // You might want to validate the expression to prevent sql injection (unless you can only create query expressions in code)
            return expression.GetType().Assembly.FullName.StartsWith("ExpressionFramework.Domain");
        }

        public override IQueryBuilder ToBuilder()
        {
            return new CatalogQueryBuilder(this);
        }

        public CatalogQuery() : this(null, null, new ComposedEvaluatable(Enumerable.Empty<ComposableEvaluatable>()), Enumerable.Empty<IQuerySortOrder>())
        {
        }

        public CatalogQuery(int? limit,
                            int? offset,
                            ComposedEvaluatable filter,
                            IEnumerable<IQuerySortOrder> orderByFields)
            : base(limit, offset, filter, orderByFields)
        {
        }

        public CatalogQuery(IQuery query): this(query.Limit,
                                                query.Offset,
                                                query.Filter,
                                                query.OrderByFields)
        {
        }

        private static readonly string[] ValidFieldNames = new[] { "Id", "Name", "DateCreated", "DateLastModified", "DateSynchronized", "DriveSerialNumber", "DriveTypeCodeType", "DriveTypeCode", "DriveTypeDescription", "DriveTotalSize", "DriveFreeSpace", "Recursive", "Sorted", "StartDirectory", "ExtraField1", "ExtraField2", "ExtraField3", "ExtraField4", "ExtraField5", "ExtraField6", "ExtraField7", "ExtraField8", "ExtraField9", "ExtraField10", "ExtraField11", "ExtraField12", "ExtraField13", "ExtraField14", "ExtraField15", "ExtraField16", "IsExistingEntity", "AllFields" };

        private const int MaxLimit = int.MaxValue;
    }
}

