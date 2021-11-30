using System.CodeDom.Compiler;
using System.Collections.Generic;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core;
using CrossCutting.Data.Core.Builders;
using DataFramework.ModelFramework.Poc.PagedDatabaseEntityRetrieverSettings;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial class ExtraFieldRepository : Repository<ExtraField, ExtraFieldIdentity>, IExtraFieldRepository
    {
        public ExtraFieldRepository(IDatabaseCommandProcessor<ExtraField> commandProcessor,
                                    IDatabaseEntityRetriever<ExtraField> entityRetriever,
                                    IDatabaseCommandProvider<ExtraFieldIdentity> identitySelectCommandProvider,
                                    IPagedDatabaseCommandProvider pagedEntitySelectCommandProvider,
                                    IDatabaseCommandProvider entitySelectCommandProvider,
                                    IDatabaseCommandProvider<ExtraField> entityCommandProvider)
            : base(commandProcessor, entityRetriever, identitySelectCommandProvider, pagedEntitySelectCommandProvider, entitySelectCommandProvider, entityCommandProvider)
        {
        }

        public IReadOnlyCollection<ExtraField> FindExtraFieldsByEntityName(string entityName)
        {
            var settings = new ExtraFieldQueryProcessorSettings();
            return EntityRetriever.FindMany(new SelectCommandBuilder()
                .Select(settings.Fields)
                .From(settings.TableName)
                .Where("EntityName = @entityName")
                .AppendParameter(nameof(entityName), entityName)
                .OrderBy(settings.DefaultOrderBy)
                .Build());
        }
    }
}
