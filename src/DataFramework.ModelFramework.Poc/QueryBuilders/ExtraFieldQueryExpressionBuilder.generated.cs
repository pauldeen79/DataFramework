using System;
using System.CodeDom.Compiler;
using System.Linq;
using DataFramework.ModelFramework.Poc.Repositories;
using QueryFramework.Abstractions;
using QueryFramework.Abstractions.Builders;
using QueryFramework.Core;

namespace PDC.Net.Core.QueryBuilders
{
#nullable enable
    public class ExtraFieldQueryExpressionBuilder : IQueryExpressionBuilder
    {
        public string FieldName
        {
            get;
            set;
        }

        public string? Expression
        {
            get;
            set;
        }

        public IQueryExpression Build()
        {
            return new QueryExpression(ProcessFieldName(FieldName), Expression);
        }

        public string ProcessFieldName(string fieldName)
        {
            if (!fieldName.StartsWith("ExtraField", StringComparison.InvariantCultureIgnoreCase))
            {
                var extraFields = _extraFieldRepository.FindExtraFieldsByEntityName(_entityName);
                var extraField = extraFields.FirstOrDefault(x => x.Name.Equals(fieldName, StringComparison.InvariantCultureIgnoreCase));
                if (extraField != null)
                {
                    return string.Format("ExtraField{0}", extraField.FieldNumber);
                }
            }
            return fieldName;
        }

        public ExtraFieldQueryExpressionBuilder(IExtraFieldRepository extraFieldRepository, string entityName, IQueryExpression? source = null)
        {
            _extraFieldRepository = extraFieldRepository;
            _entityName = entityName;
            if (source != null)
            {
                FieldName = source.FieldName;
                Expression = !(source is IExpressionContainer expressionContainer)
                    ? source.Expression
                    : expressionContainer.SourceExpression;
            }
            else
            {
                FieldName = string.Empty;
            }
        }

        public ExtraFieldQueryExpressionBuilder(IExtraFieldRepository extraFieldRepository, string entityName, string fieldName, string? expression = null)
        {
            _extraFieldRepository = extraFieldRepository;
            _entityName = entityName;
            FieldName = fieldName;
            Expression = expression;
        }

        private readonly IExtraFieldRepository _extraFieldRepository;

        private readonly string _entityName;
    }
#nullable restore
}

