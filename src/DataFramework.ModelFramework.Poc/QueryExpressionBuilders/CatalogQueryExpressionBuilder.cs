using System;
using System.Collections.Generic;
using PDC.Net.Core.QueryExpressions;
using QueryFramework.Abstractions;
using QueryFramework.Abstractions.Builders;
using QueryFramework.Core;

namespace PDC.Net.Core.QueryExpressionBuilders
{
    public class CatalogQueryExpressionBuilder : IQueryExpressionBuilder
    {
        private static readonly IDictionary<string, string> _customFields = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
        {
            { "AllFields", "[Name] + ' ' + [StartDirectory] + ' ' + COALESCE([ExtraField1], '') + ' ' + COALESCE([ExtraField2], '') + ' ' + COALESCE([ExtraField3], '') + ' ' + COALESCE([ExtraField4], '') + ' ' + COALESCE([ExtraField5], '') + ' ' + COALESCE([ExtraField6], '') + ' ' + COALESCE([ExtraField7], '') + ' ' + COALESCE([ExtraField8], '') + ' ' + COALESCE([ExtraField9], '') + ' ' + COALESCE([ExtraField10], '') + ' ' + COALESCE([ExtraField11], '') + ' ' + COALESCE([ExtraField12], '') + ' ' + COALESCE([ExtraField13], '') + ' ' + COALESCE([ExtraField14], '') + ' ' + COALESCE([ExtraField15], '') + ' ' + COALESCE([ExtraField16], '')" },
        };

        public string FieldName { get; set; }
        public string? Expression { get; set; }

        public IQueryExpression Build()
        {
            return ProcessFieldName();
        }

        public CatalogQueryExpressionBuilder()
        {
            FieldName = string.Empty;
        }
        public CatalogQueryExpressionBuilder(IQueryExpression source)
        {
            FieldName = source.FieldName;
            Expression = !(source is IExpressionContainer expressionContainer)
                ? source.Expression
                : expressionContainer.SourceExpression;
        }

        public CatalogQueryExpressionBuilder(string fieldName, string? expression = null)
        {
            FieldName = fieldName;
            Expression = expression;
        }

        private IQueryExpression ProcessFieldName()
        {
            var customExpressionFound = _customFields.TryGetValue(FieldName, out var customExpression);
            if (customExpressionFound)
            {
                return new PdcCustomQueryExpression(customExpression, Expression);
            }
            return new QueryExpression(FieldName, Expression);
        }
    }
}
