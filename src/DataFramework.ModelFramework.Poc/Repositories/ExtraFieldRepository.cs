using System.CodeDom.Compiler;
using System.Collections.Generic;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class ExtraFieldRepository : Repository<ExtraField, ExtraFieldIdentity>, IExtraFieldRepository
    {
        public ExtraFieldRepository(IDatabaseCommandProcessor<ExtraField> databaseCommandProcessor, IDatabaseEntityRetriever<ExtraField> entityRetriever, IDatabaseCommandProvider<ExtraFieldIdentity> databaseCommandProvider) : base(databaseCommandProcessor, entityRetriever, databaseCommandProvider)
        {
        }

        public IReadOnlyCollection<ExtraField> FindExtraFieldsByEntityName(string entityName)
        {
            var settings = new ExtraFieldQueryProcessorSettings();
            return _entityRetriever.FindMany(new SqlTextCommand(string.Format("SELECT {0} FROM {1} WHERE EntityName = @entityName ORDER BY {2}", settings.Fields, settings.TableName, settings.DefaultOrderBy), new { entityName }));
        }
    }
}
