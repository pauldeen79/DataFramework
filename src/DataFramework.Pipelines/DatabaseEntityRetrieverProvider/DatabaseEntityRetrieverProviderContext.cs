﻿namespace DataFramework.Pipelines.DatabaseEntityRetrieverProvider;

public class DatabaseEntityRetrieverProviderContext : ContextBase
{
    public DatabaseEntityRetrieverProviderContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
    }

    public ClassBuilder Builder => _wrappedBuilder.Builder;

    private readonly ClassBuilderWrapper _wrappedBuilder = new();
}
