using System.CodeDom.Compiler;
using System.Collections.Generic;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core;
using CrossCutting.Data.Core.Commands;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class ExtraFieldRepository : Repository<ExtraField, ExtraFieldIdentity>, IExtraFieldRepository
    {
        public ExtraFieldRepository(IDatabaseCommandProcessor<ExtraField> databaseCommandProcessor,
                                    IDatabaseEntityRetriever<ExtraField> entityRetriever,
                                    IPagedDatabaseCommandProvider<ExtraFieldIdentity> identityDatabaseCommandProvider,
                                    IDatabaseCommandProvider<ExtraField> entityDatabaseCommandProvider)
            : base(databaseCommandProcessor, entityRetriever, identityDatabaseCommandProvider, entityDatabaseCommandProvider)
        {
        }

        public IReadOnlyCollection<ExtraField> FindExtraFieldsByEntityName(string entityName)
        {
            var settings = new ExtraFieldQueryProcessorSettings();
            return EntityRetriever.FindMany(new SqlTextCommand(string.Format("SELECT {0} FROM {1} WHERE EntityName = @entityName ORDER BY {2}", settings.Fields, settings.TableName, settings.DefaultOrderBy), new { entityName }));
        }
    }
}
