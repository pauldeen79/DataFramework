﻿namespace DataFramework.Pipelines.Abstractions;

public interface IPipelineService
{
    Task<Result<TypeBase>> Process(ClassContext context, CancellationToken cancellationToken);
}
