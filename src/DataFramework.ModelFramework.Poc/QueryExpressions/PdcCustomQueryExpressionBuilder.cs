using System.Diagnostics.CodeAnalysis;
using QueryFramework.Abstractions;
using QueryFramework.Abstractions.Builders;

namespace PDC.Net.Core.QueryExpressions
{
    [ExcludeFromCodeCoverage]
    internal sealed class PdcCustomQueryExpressionBuilder : IQueryExpressionBuilder
    {
        public PdcCustomQueryExpressionBuilder()
        {
            FieldName = string.Empty;
        }
        public PdcCustomQueryExpressionBuilder(PdcCustomQueryExpression pdcCustomQueryExpression)
        {
            FieldName = pdcCustomQueryExpression.FieldName;
            Expression = !(pdcCustomQueryExpression is IExpressionContainer expressionContainer)
                ? pdcCustomQueryExpression.Expression
                : expressionContainer.SourceExpression;
        }

        public string? Expression { get; set; }
        public string FieldName { get; set; }

        public IQueryExpression Build()
            => new PdcCustomQueryExpression(FieldName, Expression);
    }
}
