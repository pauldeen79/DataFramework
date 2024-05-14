using System.CodeDom.Compiler;
using System.Collections.Generic;
using CrossCutting.Data.Abstractions;
using CrossCutting.Data.Core;
using DataFramework.ModelFramework.Poc.PagedDatabaseEntityRetrieverSettings;
using PDC.Net.Core.Entities;
using PDC.Net.Core.Queries;
using QueryFramework.Abstractions;
using QueryFramework.Abstractions.Builders.Extensions;

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
                                    IDatabaseCommandProvider<ExtraField> entityCommandProvider,
                                    IQueryProcessor queryProcessor)
            : base(commandProcessor, entityRetriever, identitySelectCommandProvider, pagedEntitySelectCommandProvider, entitySelectCommandProvider, entityCommandProvider)
        {
            QueryProcessor = queryProcessor;
        }

        public IQueryProcessor QueryProcessor { get; }

        public IReadOnlyCollection<ExtraField> FindExtraFieldsByEntityName(string entityName)
        {
            var settings = new ExtraFieldPagedEntityRetrieverSettings();

            //return EntityRetriever.FindMany(new SelectCommandBuilder()
            //    .Select("*")
            //    .From(settings.TableName)
            //    .Where("EntityName = @entityName")
            //    .AppendParameter(nameof(entityName), entityName)
            //    .OrderBy(settings.DefaultOrderBy)
            //    .Build());

            var query = new ExtraFieldQueryBuilder().Where(nameof(ExtraField.EntityName)).IsEqualTo(entityName).OrderBy(settings.DefaultOrderBy).Build();
            return QueryProcessor.FindMany<ExtraField>(query);
        }
    }
}
