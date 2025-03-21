namespace DataFramework.ModelFramework.Poc.Tests;

public sealed partial class IntegrationTests
{
    [Fact]
    public void Can_Query_Database_Using_Basic_Query()
    {
        // Arrange
        Connection.AddResultForDataReader(cmd => cmd.CommandText.StartsWith("SELECT") && cmd.CommandText.Contains(" FROM [Catalog]"),
                                          () => new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });
        var query = new CatalogQuery(new SingleEntityQueryBuilder()
            .Where(nameof(Catalog.Name)).StartsWith("Diversen cd")
            .Build());

        // Act
        var actual = QueryProcessor.FindMany<Catalog>(query);

        // Assert
        actual.ShouldHaveSingleItem();
        actual.First().IsExistingEntity.ShouldBeTrue(); //set from CatalogDatabaseCommandEntityProvider
    }

    [Fact]
    public async Task Can_Query_Database_Using_Basic_Query_Async()
    {
        // Arrange
        Connection.AddResultForDataReader(cmd => cmd.CommandText.StartsWith("SELECT") && cmd.CommandText.Contains(" FROM [Catalog]"),
                                          () => new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });
        var query = new CatalogQuery(new SingleEntityQueryBuilder()
            .Where(nameof(Catalog.Name)).StartsWith("Diversen cd")
            .Build());

        // Act
        var actual = await QueryProcessor.FindManyAsync<Catalog>(query);

        // Assert
        actual.ShouldHaveSingleItem();
        actual.First().IsExistingEntity.ShouldBeTrue(); //set from CatalogDatabaseCommandEntityProvider
    }

    [Fact]
    public void Can_Query_Database_Using_ExtraFieldNames()
    {
        // Arrange
        Connection.AddResultForDataReader(cmd => cmd.CommandText.StartsWith("SELECT") && cmd.CommandText.Contains(" FROM [Catalog]") && cmd.CommandText.Contains(" WHERE ExtraField1 = @p0 "),
                                          () => new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", "Value", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });
        var query = new CatalogQuery(new SingleEntityQueryBuilder()
            .Where("MyField").IsEqualTo("Value")
            .Build());

        // Act
        var actual = QueryProcessor.FindMany<Catalog>(query);

        // Assert
        actual.ShouldHaveSingleItem();
        actual.First().IsExistingEntity.ShouldBeTrue(); //set from CatalogEntityMapper
        actual.First().ExtraField1.ShouldBe("Value");
    }

    [Fact]
    public void Can_Query_Database_Using_CustomQueryExpression()
    {
        // Arrange
        Connection.AddResultForDataReader(cmd => cmd.CommandText.StartsWith("SELECT") && cmd.CommandText.Contains("WHERE CHARINDEX(@p0, [Name] + ' ' + [StartDirectory] + ' ' + COALESCE([ExtraField1], '') + ' ' + COALESCE([ExtraField2], '') + ' ' + COALESCE([ExtraField3], '') + ' ' + COALESCE([ExtraField4], '') + ' ' + COALESCE([ExtraField5], '') + ' ' + COALESCE([ExtraField6], '') + ' ' + COALESCE([ExtraField7], '') + ' ' + COALESCE([ExtraField8], '') + ' ' + COALESCE([ExtraField9], '') + ' ' + COALESCE([ExtraField10], '') + ' ' + COALESCE([ExtraField11], '') + ' ' + COALESCE([ExtraField12], '') + ' ' + COALESCE([ExtraField13], '') + ' ' + COALESCE([ExtraField14], '') + ' ' + COALESCE([ExtraField15], '') + ' ' + COALESCE([ExtraField16], '')) > 0"),
                                          () => new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });
        var query = new CatalogQuery(new SingleEntityQueryBuilder()
            .Where("AllFields").Contains("Diversen")
            .Build());

        // Act
        var actual = QueryProcessor.FindMany<Catalog>(query);

        // Assert
        actual.ShouldHaveSingleItem();
        actual.First().IsExistingEntity.ShouldBeTrue(); //set from CatalogEntityMapper
    }

    [Fact]
    public void Can_Use_Internal_Expression_From_ExpressionFramework()
    {
        // Arrange
        Connection.AddResultForDataReader(cmd => cmd.CommandText.StartsWith("SELECT") && cmd.CommandText.Contains("WHERE LEN([Name] + ' ' + [StartDirectory] + ' ' + COALESCE([ExtraField1], '') + ' ' + COALESCE([ExtraField2], '') + ' ' + COALESCE([ExtraField3], '') + ' ' + COALESCE([ExtraField4], '') + ' ' + COALESCE([ExtraField5], '') + ' ' + COALESCE([ExtraField6], '') + ' ' + COALESCE([ExtraField7], '') + ' ' + COALESCE([ExtraField8], '') + ' ' + COALESCE([ExtraField9], '') + ' ' + COALESCE([ExtraField10], '') + ' ' + COALESCE([ExtraField11], '') + ' ' + COALESCE([ExtraField12], '') + ' ' + COALESCE([ExtraField13], '') + ' ' + COALESCE([ExtraField14], '') + ' ' + COALESCE([ExtraField15], '') + ' ' + COALESCE([ExtraField16], '')) > @p0"),
                                          () => new[] { new Catalog(1, "Diversen cd 1", DateTime.Today, DateTime.Now, DateTime.Now, "0000-0000", "CDT", "CDR", "CD-ROM", 1, 2, true, true, @"C:\", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null) });
        var query = new CatalogQuery(new SingleEntityQueryBuilder()
            .Where("AllFields").Len().IsGreaterThan(4)
            .Build());

        // Act
        var actual = QueryProcessor.FindMany<Catalog>(query);

        // Assert
        actual.ShouldHaveSingleItem();
        actual.First().IsExistingEntity.ShouldBeTrue(); //set from CatalogEntityMapper
    }
}
