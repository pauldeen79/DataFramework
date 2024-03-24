using System.CodeDom.Compiler;
using System.Linq;
using QueryFramework.Abstractions;
using QueryFramework.Core.Builders;

namespace PDC.Net.Core.Queries
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Entities.QueryBuilderGenerator", @"1.0.0.0")]
    public class ExtraFieldQueryBuilder : QueryBuilder
    {
        public ExtraFieldQueryBuilder()
        {
        }

        public ExtraFieldQueryBuilder(IQuery source) : base(source)
        {
        }

        public override IQuery Build()
        {
            return BuildTyped();
        }

        public IQuery BuildTyped()
        {
            return new ExtraFieldQuery(Limit, Offset, Filter?.BuildTyped(), OrderByFields?.Select(x => x.Build()));
        }
    }
}
