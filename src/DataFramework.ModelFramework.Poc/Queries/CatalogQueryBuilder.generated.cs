using System.CodeDom.Compiler;
using System.Linq;
using QueryFramework.Abstractions;
using QueryFramework.Core.Builders;

namespace PDC.Net.Core.Queries
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Entities.QueryBuilderGenerator", @"1.0.0.0")]
    public class CatalogQueryBuilder : QueryBuilder
    {
        public CatalogQueryBuilder()
        {
        }

        public CatalogQueryBuilder(IQuery source) : base(source)
        {
        }

        public override IQuery Build()
        {
            return BuildTyped();
        }

        public CatalogQuery BuildTyped()
        {
            return new CatalogQuery(Limit, Offset, Filter?.BuildTyped(), OrderByFields?.Select(x => x.Build()));
        }
    }
}
