namespace DataFramework.ModelFramework.Poc.Tests;

public sealed partial class IntegrationTests
{
    [Fact]
    public void Can_Query_Database_Using_Command()
    {
        // Arrange
        Connection.AddResultForDataReader(new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });

        // Act
        var actual = Retriever.FindMany(new SelectCommandBuilder()
            .Select("*")
            .From("Catalog")
            .Where("LEFT(Name, 11) = @p0")
            .AppendParameter("p0", "Diversen cd")
            .Build());

        // Assert
        actual.ShouldHaveSingleItem();
        actual.First().IsExistingEntity.ShouldBeTrue(); //set from CatalogEntityMapper
    }

    [Fact]
    public async Task Can_Query_Database_Using_Command_Async()
    {
        // Arrange
        Connection.AddResultForDataReader(new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });

        // Act
        var actual = await Retriever.FindManyAsync(new SelectCommandBuilder()
            .Select("*")
            .From("Catalog")
            .Where("LEFT(Name, 11) = @p0")
            .AppendParameter("p0", "Diversen cd")
            .Build());

        // Assert
        actual.ShouldHaveSingleItem();
        actual.First().IsExistingEntity.ShouldBeTrue(); //set from CatalogEntityMapper
    }
}
