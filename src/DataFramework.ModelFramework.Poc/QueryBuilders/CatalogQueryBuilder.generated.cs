using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using PDC.Net.Core.Queries;
using QueryFramework.Abstractions.Builders;
using QueryFramework.Abstractions.Queries.Builders;

namespace PDC.Net.Core.QueryBuilders
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Entities.QueryBuilderGenerator", @"1.0.0.0")]
    public partial class CatalogQueryBuilder : ISingleEntityQueryBuilder
    {
        public int? Limit
        {
            get;
            set;
        }

        public int? Offset
        {
            get;
            set;
        }

        public List<IQueryConditionBuilder> Conditions
        {
            get;
            set;
        }

        public List<IQuerySortOrderBuilder> OrderByFields
        {
            get;
            set;
        }

        public CatalogQuery Build()
        {
            return new CatalogQuery(Limit, Offset, Conditions.Select(x => x.Build()), OrderByFields.Select(x => x.Build()));
        }

        public CatalogQueryBuilder()
        {
            Conditions = new List<IQueryConditionBuilder>();
            OrderByFields = new List<IQuerySortOrderBuilder>();
        }
    }
}

