using System;
using System.Collections.Generic;
using System.Data.Stub;
using System.Data.Stub.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CrossCutting.Data.Sql;
using DataFramework.ModelFramework.Poc.Repositories;
using FluentAssertions;
using Moq;
using PDC.Net.Core.Entities;
using PDC.Net.Core.Queries;
using PDC.Net.Core.QueryBuilders;
using QueryFramework.Abstractions;
using QueryFramework.Core.Extensions;
using QueryFramework.Core.Queries.Builders.Extensions;
using QueryFramework.SqlServer;
using QueryFramework.SqlServer.Abstractions;
using Xunit;

namespace DataFramework.ModelFramework.Poc.Tests
{
    [ExcludeFromCodeCoverage]
    public class IntegrationTests
    {
        [Fact]
        public void Can_Query_Database()
        {
            // Arrange
            var settings = new CatalogQueryProcessorSettings();

            // TODO: Generate a default query field provider in QueryFramework.SqlServer, and use it here
            var fieldProviderMock = new Mock<IQueryFieldProvider>();
            fieldProviderMock.Setup(x => x.GetSelectFields(It.IsAny<IEnumerable<string>>()))
                             .Returns<IEnumerable<string>>(input => input);
            fieldProviderMock.Setup(x => x.GetAllFields())
                             .Returns(Enumerable.Empty<string>());
            fieldProviderMock.Setup(x => x.ValidateExpression(It.IsAny<IQueryExpression>()))
                             .Returns(true);
            fieldProviderMock.Setup(x => x.GetDatabaseFieldName(It.IsAny<string>()))
                             .Returns<string>(x => x);

            var databaseCommandGenerator = new DatabaseCommandGenerator(fieldProviderMock.Object);
            using var connection = new DbConnection();
            connection.AddResultForDataReader(new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });
            var mapper = new CatalogEntityMapper();
            var retriever = new DatabaseEntityRetriever<Catalog>(connection, mapper);
            var queryProcessor = new QueryProcessor<CatalogQuery, Catalog>(retriever, settings, databaseCommandGenerator);
            var provider = new CatalogDatabaseCommandEntityProvider();
            var databaseCommandProcessor = new DatabaseCommandProcessor<Catalog, CatalogBuilder>(connection, provider);
            var sut = new CatalogRepository(queryProcessor, retriever, databaseCommandProcessor);

            // Act
            var actual = sut.FindMany(new CatalogQueryBuilder().Where(nameof(Catalog.Name).DoesStartWith("Diversen cd")).Build());

            // Assert
            actual.Should().NotBeEmpty();
            actual.First().IsExistingEntity.Should().BeTrue(); //set from CatalogDatabaseCommandEntityProvider
        }
    }
}
