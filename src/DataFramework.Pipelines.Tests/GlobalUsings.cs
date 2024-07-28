﻿global using System.Globalization;
global using System.Text;
global using AutoFixture;
global using AutoFixture.AutoNSubstitute;
global using AutoFixture.Kernel;
global using ClassFramework.Domain;
global using ClassFramework.Domain.Builders.Extensions;
global using ClassFramework.Pipelines.Extensions;
global using ClassFramework.TemplateFramework;
global using ClassFramework.TemplateFramework.Builders;
global using ClassFramework.TemplateFramework.CodeGenerationProviders;
global using ClassFramework.TemplateFramework.Extensions;
global using CrossCutting.Common.Results;
global using CrossCutting.ProcessingPipeline;
global using CrossCutting.Utilities.Parsers.Extensions;
global using CsharpExpressionDumper.Core.Extensions;
global using DatabaseFramework.Domain.Abstractions;
global using DatabaseFramework.TemplateFramework;
global using DatabaseFramework.TemplateFramework.Builders;
global using DatabaseFramework.TemplateFramework.CodeGenerationProviders;
global using DatabaseFramework.TemplateFramework.Extensions;
global using DataFramework.Domain;
global using DataFramework.Domain.Builders;
global using DataFramework.Pipelines.Builders;
global using DataFramework.Pipelines.Class;
global using DataFramework.Pipelines.CommandEntityProvider;
global using DataFramework.Pipelines.CommandProvider;
global using DataFramework.Pipelines.DatabaseEntityRetrieverProvider;
global using DataFramework.Pipelines.DatabaseSchema;
global using DataFramework.Pipelines.Domains;
global using DataFramework.Pipelines.EntityMapper;
global using DataFramework.Pipelines.Extensions;
global using DataFramework.Pipelines.IdentityClass;
global using DataFramework.Pipelines.IdentityCommandProvider;
global using DataFramework.Pipelines.PagedEntityRetrieverSettings;
global using FluentAssertions;
global using Microsoft.Extensions.DependencyInjection;
global using NSubstitute;
global using TemplateFramework.Abstractions.CodeGeneration;
global using TemplateFramework.Abstractions.Extensions;
global using TemplateFramework.Core.CodeGeneration;
global using TemplateFramework.Core.CodeGeneration.Extensions;
global using TemplateFramework.Core.Extensions;
global using TemplateFramework.Core.GenerationEnvironments;
global using TemplateFramework.Runtime.Abstractions;
global using TemplateFramework.Runtime.Extensions;
global using TemplateFramework.TemplateProviders.ChildTemplateProvider.Extensions;
global using Xunit;
