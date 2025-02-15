using System.CodeDom.Compiler;
using System.Linq;
using QueryFramework.Abstractions;
using QueryFramework.Core;
using QueryFramework.Core.Builders;

namespace PDC.Net.Core.Queries
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Entities.QueryBuilderGenerator", @"1.0.0.0")]
    public class CatalogQueryBuilder : QueryBuilder
    {
        public CatalogQueryBuilder() : base()
        {
        }

        public CatalogQueryBuilder(IQuery source) : base(source)
        {
        }

        public override Query Build()
        {
            return BuildTyped();
        }

        public CatalogQuery BuildTyped()
        {
            return new CatalogQuery(Limit, Offset, Filter?.BuildTyped(), OrderByFields?.Select(x => x.Build()));
        }
    }
}
