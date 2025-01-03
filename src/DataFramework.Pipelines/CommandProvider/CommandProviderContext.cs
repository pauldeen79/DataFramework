﻿namespace DataFramework.Pipelines.CommandProvider;

public class CommandProviderContext : ContextBase
{
    public CommandProviderContext(DataObjectInfo sourceModel, PipelineSettings settings, IFormatProvider formatProvider)
        : base(sourceModel, settings, formatProvider)
    {
    }

    public ClassBuilder Builder => _wrappedBuilder.Builder;

    private readonly ClassBuilderWrapper _wrappedBuilder = new();
}
