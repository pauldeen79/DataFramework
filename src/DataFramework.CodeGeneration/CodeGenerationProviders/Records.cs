﻿using ModelFramework.CodeGeneration.Tests.CodeGenerationProviders;
using TextTemplateTransformationFramework.Runtime.CodeGeneration;

namespace DataFramework.CodeGeneration.CodeGenerationProviders
{
    public class Records : DataFrameworkCSharpClassBase, ICodeGenerationProvider
    {
        public override string Path => "DataFramework.Core";

        public override string DefaultFileName => "Entities.generated.cs";

        public override bool RecurseOnDeleteGeneratedFiles => false;

        public override object CreateModel()
            => GetImmutableClasses(GetDataFrameworkModelTypes(), "DataFramework.Core");
    }
}