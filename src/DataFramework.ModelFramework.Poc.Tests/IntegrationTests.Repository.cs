using System;
using System.Data;
using System.Data.Stub;
using System.Data.Stub.Extensions;
using System.Linq;
using FluentAssertions;
using PDC.Net.Core.Entities;
using Xunit;

namespace DataFramework.ModelFramework.Poc.Tests
{
    public sealed partial class IntegrationTests
    {
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
        public void Can_Find_All_Entities()
        {
            // Arrange
            Connection.AddResultForDataReader(new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });

            // Act
            var actual = Repository.FindAll();

            // Assert
            actual.Should().ContainSingle();
            actual.First().IsExistingEntity.Should().BeTrue(); //set from CatalogEntityMapper
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
    }
}
