﻿namespace DataFramework.Pipelines.Abstractions;

public interface IPipelineService
{
    Task<Result<TypeBase>> Process(EntityContext context, CancellationToken cancellationToken);
}