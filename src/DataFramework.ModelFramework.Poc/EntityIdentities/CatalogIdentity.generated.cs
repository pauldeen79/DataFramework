﻿using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PDC.Net.Core.Entities
{
    [GeneratedCode(@"DataFramework.ModelFramework.Generators.Entities.EntityIdentityGenerator", @"1.0.0.0")]
    [ExcludeFromCodeCoverage]
    public partial record CatalogIdentity
    {
        public int Id
        {
            get;
        }

        public CatalogIdentity(Catalog instance)
        {
            Id = instance.Id;
            Validator.ValidateObject(this, new ValidationContext(this, null, null), true);
        }

        public CatalogIdentity(int id)
        {
            Id = id;
            Validator.ValidateObject(this, new ValidationContext(this, null, null), true);
        }
    }
}

