using PDC.Net.Core.Entities;
using PDC.Net.Core.Queries;
using PDC.Net.Core.QueryBuilders;
using QueryFramework.Core.Extensions;
using QueryFramework.Core.Queries.Builders.Extensions;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    public class CatalogQueryProvider : IQueryProvider<CatalogIdentity, CatalogQuery>
    {
        public CatalogQuery Create(CatalogIdentity source)
        {
            /// old code: return FindOne(new SqlTextCommand(string.Format(@"SELECT TOP 1 {0} FROM {1} WHERE [Id] = @Id", SelectFields, TableAlias), identity));
            return new CatalogQueryBuilder().Take(1).Where("Id".IsEqualTo(source.Id)).Build();
        }
    }
}
