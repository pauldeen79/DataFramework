using System.CodeDom.Compiler;
using System.Collections.Generic;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial interface IExtraFieldRepository
    {
        ExtraField Add(ExtraField instance);

        ExtraField Update(ExtraField instance);

        ExtraField Delete(ExtraField instance);

        ExtraField Find(ExtraFieldIdentity identity);

        IReadOnlyCollection<ExtraField> FindExtraFieldsByEntityName(string entityName);
    }
}

