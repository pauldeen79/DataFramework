using System.CodeDom.Compiler;
using System.Collections.Generic;
using PDC.Net.Core.Entities;

namespace DataFramework.ModelFramework.Poc.Repositories
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Repositories.RepositoryGenerator", @"1.0.0.0")]
    public partial interface IExtraFieldRepository : IRepository<ExtraField, ExtraFieldIdentity>
    {
        IReadOnlyCollection<ExtraField> FindExtraFieldsByEntityName(string entityName);
    }
}

