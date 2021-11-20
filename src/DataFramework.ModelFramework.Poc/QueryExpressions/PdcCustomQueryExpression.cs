using QueryFramework.Abstractions;
using QueryFramework.Abstractions.Builders;

namespace PDC.Net.Core.QueryExpressions
{
    internal sealed class PdcCustomQueryExpression : IQueryExpression, IExpressionContainer, ICustomQueryExpression
    {
        private readonly string? _expression;

        public PdcCustomQueryExpression(string fieldName, string? expression = null)
        {
            FieldName = fieldName;
            _expression = expression;
        }

        /// <summary>Gets the name of the field.</summary>
        /// <value>The name of the field.</value>
        public string FieldName { get; }

        /// <summary>Gets the expression.</summary>
        /// <value>The expression.</value>
        public string Expression 
            => _expression == null
                ? FieldName
                : string.Format(_expression, FieldName);

        string? IExpressionContainer.SourceExpression
            => _expression;

        public IQueryExpressionBuilder CreateBuilder()
            => new PdcCustomQueryExpressionBuilder(this);

        public override string ToString()
            => Expression;
    }
}
