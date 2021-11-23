using System;
using System.Data.Stub;
using System.Data.Stub.Extensions;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CrossCutting.Data.Core;
using CrossCutting.Data.Core.Builders;
using CrossCutting.Data.Sql;
using DataFramework.ModelFramework.Poc.DatabaseCommandEntityProviders;
using DataFramework.ModelFramework.Poc.DatabaseCommandProviders;
using DataFramework.ModelFramework.Poc.EntityMappers;
using DataFramework.ModelFramework.Poc.PagedDatabaseCommandProviders;
using DataFramework.ModelFramework.Poc.QueryProcessorSettings;
using DataFramework.ModelFramework.Poc.Repositories;
using FluentAssertions;
using PDC.Net.Core.Entities;
using PDC.Net.Core.Queries;
using PDC.Net.Core.QueryBuilders;
using PDC.Net.Core.QueryViewModels;
using QueryFramework.Abstractions;
using QueryFramework.Abstractions.Extensions.Builders;
using QueryFramework.Core.Builders;
using QueryFramework.Core.Extensions;
using QueryFramework.Core.Queries.Builders.Extensions;
using QueryFramework.SqlServer;
using Xunit;

namespace DataFramework.ModelFramework.Poc.Tests
{
    [ExcludeFromCodeCoverage]
    public sealed class IntegrationTests : IDisposable
    {
        private Repository<Catalog, CatalogIdentity> Repository { get; }
        private DatabaseEntityRetriever<Catalog> Retriever { get; }
        private QueryProcessor<CatalogQuery, Catalog> QueryProcessor { get; }
        private DbConnection Connection { get; }

        public IntegrationTests()
        {
            var settings = new CatalogQueryProcessorSettings();
            var fieldProvider = new DefaultQueryFieldProvider();
            var databaseCommandGenerator = new DatabaseCommandGenerator(fieldProvider);
            Connection = new DbConnection();
            var mapper = new CatalogEntityMapper();
            Retriever = new DatabaseEntityRetriever<Catalog>(Connection, mapper);
            QueryProcessor = new QueryProcessor<CatalogQuery, Catalog>(Retriever, settings, databaseCommandGenerator);
            var commandEntityProvider = new CatalogDatabaseCommandEntityProvider();
            var databaseCommandProcessor = new DatabaseCommandProcessor<Catalog, CatalogBuilder>(Connection, commandEntityProvider);
            var catalogIdentityDatabaseCommandProvider = new CatalogIdentityDatabaseCommandProvider();
            var catalogDatabaseCommandProvider = new CatalogDatabaseCommandProvider();
            Repository = new CatalogRepository(databaseCommandProcessor,
                                                Retriever,
                                                catalogIdentityDatabaseCommandProvider,
                                                catalogDatabaseCommandProvider);
        }

        #region QueryProcessor
        [Fact]
        public void Can_Query_Database_Using_Query()
        {
            // Arrange
            Connection.AddResultForDataReader(new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });

            // Act
            var actual = QueryProcessor.FindMany(new CatalogQueryBuilder().Where(nameof(Catalog.Name).DoesStartWith("Diversen cd")).Build());

            // Assert
            actual.Should().ContainSingle();
            actual.First().IsExistingEntity.Should().BeTrue(); //set from CatalogDatabaseCommandEntityProvider
        }

        [Fact]
        public void Can_Query_Database_Using_ExtraFieldNames()
        {
            // Arrange
            Connection.AddResultForDataReader(cmd => cmd.CommandText.Contains(" WHERE ExtraField1 = @p0 "), new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });
            Connection.AddResultForDataReader(cmd => cmd.CommandText.Contains(" FROM [ExtraField]"), new[] { new ExtraField("Catalog", "MyField", null, 1, typeof(string).FullName, true) });
            var commandEntityProvider = new ExtraFieldDatabaseCommandEntityProvider();
            var databaseCommandProcessor = new DatabaseCommandProcessor<ExtraField, ExtraFieldBuilder>(Connection, commandEntityProvider);
            var mapper = new ExtraFieldEntityMapper();
            var retriever = new DatabaseEntityRetriever<ExtraField>(Connection, mapper);
            var extraFieldIdentityDatabaseCommandProvider = new ExtraFieldIdentityDatabaseCommandProvider();
            var extraFieldEntityDatabaseCommandProvider = new ExtraFieldDatabaseCommandProvider();
            var extraFieldRepository = new ExtraFieldRepository(databaseCommandProcessor,
                                                                retriever,
                                                                extraFieldIdentityDatabaseCommandProvider,
                                                                extraFieldEntityDatabaseCommandProvider);
            var queryViewModel = new CatalogQueryViewModel(extraFieldRepository, QueryProcessor);
            queryViewModel.Conditions.Add(new QueryConditionBuilder()
                .WithField("MyField")
                .WithOperator(QueryOperator.Equal)
                .WithValue("Value"));

            // Act
            var actual = QueryProcessor.FindMany(queryViewModel.CreateQuery());

            // Assert
            actual.Should().ContainSingle();
            actual.First().IsExistingEntity.Should().BeTrue(); //set from CatalogEntityMapper
        }

        [Fact]
        public void Can_Query_Database_Using_CustomQueryExpression()
        {
            // Arrange
            Connection.AddResultForDataReader(cmd => cmd.CommandText.Contains("WHERE CHARINDEX(@p0, [Name] + ' ' + [StartDirectory] + ' ' + COALESCE([ExtraField1], '') + ' ' + COALESCE([ExtraField2], '') + ' ' + COALESCE([ExtraField3], '') + ' ' + COALESCE([ExtraField4], '') + ' ' + COALESCE([ExtraField5], '') + ' ' + COALESCE([ExtraField6], '') + ' ' + COALESCE([ExtraField7], '') + ' ' + COALESCE([ExtraField8], '') + ' ' + COALESCE([ExtraField9], '') + ' ' + COALESCE([ExtraField10], '') + ' ' + COALESCE([ExtraField11], '') + ' ' + COALESCE([ExtraField12], '') + ' ' + COALESCE([ExtraField13], '') + ' ' + COALESCE([ExtraField14], '') + ' ' + COALESCE([ExtraField15], '') + ' ' + COALESCE([ExtraField16], '')) > 0"), new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });

            // Act
            var actual = QueryProcessor.FindMany(new CatalogQueryBuilder().Where("AllFields".DoesContain("Diversen")).Build());

            // Assert
            actual.Should().ContainSingle();
            actual.First().IsExistingEntity.Should().BeTrue(); //set from CatalogEntityMapper
        }
        #endregion

        #region EntityRetriever
        [Fact]
        public void Can_Query_Database_Using_Command()
        {
            // Arrange
            Connection.AddResultForDataReader(new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });

            // Act
            var actual = Retriever.FindMany(new SelectCommandBuilder().Select("*").From("Catalog").Where("LEFT(Name, 11) = @p0").AppendParameter("p0", "Diversen cd").Build());

            // Assert
            actual.Should().ContainSingle();
            actual.First().IsExistingEntity.Should().BeTrue(); //set from CatalogEntityMapper
        }
        #endregion

        #region Repository
        [Fact]
        public void Can_Find_Entity_By_Identity()
        {
            // Arrange
            Connection.AddResultForDataReader(new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });

            // Act
            var actual = Repository.Find(new CatalogIdentity(1));

            // Assert
            actual.Should().NotBeNull();
            actual.IsExistingEntity.Should().BeTrue(); //set from CatalogEntityMapper
        }

        [Fact]
        public void Can_Add_Entity_To_Database()
        {
            // Arrange
            Connection.AddResultForDataReader(new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });

            // Act
            var actual = Repository.Add(new Catalog(0, "Test", DateTime.MinValue, DateTime.MinValue, DateTime.MinValue, "0000-0000", "CDT", "CDR", null, 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null));

            // Assert
            actual.Should().NotBeNull();
            actual.Id.Should().Be(1); // read from database in CatalogDatabaseCommandEntityProvider
            actual.IsExistingEntity.Should().BeTrue(); //set from CatalogDatabaseCommandEntityProvider
        }

        [Fact]
        public void Can_Update_Entity_From_Database()
        {
            // Arrange
            const string FindSql = "SELECT [Id], [Name], [DateCreated], [DateLastModified], [DateSynchronized], [DriveSerialNumber], [DriveTypeCodeType], [DriveTypeCode], [DriveTypeDescription], [DriveTotalSize], [DriveFreeSpace], [Recursive], [Sorted], [StartDirectory], [ExtraField1], [ExtraField2], [ExtraField3], [ExtraField4], [ExtraField5], [ExtraField6], [ExtraField7], [ExtraField8], [ExtraField9], [ExtraField10], [ExtraField11], [ExtraField12], [ExtraField13], [ExtraField14], [ExtraField15], [ExtraField16] FROM (SELECT c.[Id], [Name], [DateCreated], [DateLastModified], [DateSynchronized], [DriveSerialNumber], c.[DriveTypeCodeType], c.[DriveTypeCode], c.[DriveTotalSize], c.[DriveFreeSpace], c.[Recursive], c.[Sorted], c.[StartDirectory], c.[ExtraField1], c.[ExtraField2], c.[ExtraField3], c.[ExtraField4], c.[ExtraField5], c.[ExtraField6], c.[ExtraField7], c.[ExtraField8], c.[ExtraField9], c.[ExtraField10], c.[ExtraField11], c.[ExtraField12], c.[ExtraField13], c.[ExtraField14], c.[ExtraField15], c.[ExtraField16], cd.[Description] AS [DriveTypeDescription] FROM [Catalog] c INNER JOIN [Code] cd ON c.[DriveTypeCode] = cd.[Code] AND cd.[CodeType] = 'CDT') AS [CatalogView] WHERE [Id] = @Id";
            Connection.AddResultForDataReader(cmd => cmd.CommandText == FindSql && ((int)cmd.Parameters.Cast<DbDataParameter>().First().Value) == 1, new[] { new Catalog(1, "Diversen cd 1", DateTime.Today.AddDays(-1), DateTime.Today.AddDays(-1), DateTime.Today.AddDays(-1), "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });
            Connection.AddResultForDataReader(cmd => cmd.CommandText == "[UpdateCatalog]", new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Today, DateTime.Today, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", "Value", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });
            var input = new CatalogBuilder(Repository.Find(new CatalogIdentity(1))).SetExtraField1("value").Build();

            // Act
            var actual = Repository.Update(input);

            // Assert
            actual.Should().NotBeNull();
            actual.Id.Should().Be(1); // read from database in CatalogDatabaseCommandEntityProvider
            actual.IsExistingEntity.Should().BeTrue(); //set from CatalogDatabaseCommandEntityProvider
            actual.ExtraField1.Should().Be("Value"); //read from database in CatalogDatabaseCommandEntityProvider
        }

        [Fact]
        public void Can_Delete_Entity_From_Database()
        {
            // Arrange
            const string FindSql = "SELECT [Id], [Name], [DateCreated], [DateLastModified], [DateSynchronized], [DriveSerialNumber], [DriveTypeCodeType], [DriveTypeCode], [DriveTypeDescription], [DriveTotalSize], [DriveFreeSpace], [Recursive], [Sorted], [StartDirectory], [ExtraField1], [ExtraField2], [ExtraField3], [ExtraField4], [ExtraField5], [ExtraField6], [ExtraField7], [ExtraField8], [ExtraField9], [ExtraField10], [ExtraField11], [ExtraField12], [ExtraField13], [ExtraField14], [ExtraField15], [ExtraField16] FROM (SELECT c.[Id], [Name], [DateCreated], [DateLastModified], [DateSynchronized], [DriveSerialNumber], c.[DriveTypeCodeType], c.[DriveTypeCode], c.[DriveTotalSize], c.[DriveFreeSpace], c.[Recursive], c.[Sorted], c.[StartDirectory], c.[ExtraField1], c.[ExtraField2], c.[ExtraField3], c.[ExtraField4], c.[ExtraField5], c.[ExtraField6], c.[ExtraField7], c.[ExtraField8], c.[ExtraField9], c.[ExtraField10], c.[ExtraField11], c.[ExtraField12], c.[ExtraField13], c.[ExtraField14], c.[ExtraField15], c.[ExtraField16], cd.[Description] AS [DriveTypeDescription] FROM [Catalog] c INNER JOIN [Code] cd ON c.[DriveTypeCode] = cd.[Code] AND cd.[CodeType] = 'CDT') AS [CatalogView] WHERE [Id] = @Id";
            Connection.AddResultForDataReader(cmd => cmd.CommandText == FindSql && ((int)cmd.Parameters.Cast<DbDataParameter>().First().Value) == 1, new[] { new Catalog(1, "Diversen cd 1", DateTime.Today.AddDays(-1), DateTime.Today.AddDays(-1), DateTime.Today.AddDays(-1), "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });
            Connection.AddResultForDataReader(cmd => cmd.CommandText == "[DeleteCatalog]", new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Today, DateTime.Today, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", "Value", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });
            var input = new CatalogBuilder(Repository.Find(new CatalogIdentity(1))).SetExtraField1("value").Build();

            // Act
            var actual = Repository.Delete(input);

            // Assert
            actual.Should().NotBeNull();
            actual.ExtraField1.Should().Be("value"); //CatalogDatabaseCommandEntityProvider does not change value, this is a 'hard' delete
        }
        #endregion

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
