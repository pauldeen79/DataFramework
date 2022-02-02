namespace DataFramework.ModelFramework.Tests;

public partial class IntegrationTests
{
    [Fact]
    public void Can_Generate_Basic_DatabaseSchema()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var input = new[] { CreateDataObjectInfo(default(EntityClassType)) }.ToSchemas(settings);

        // Act
        var actual = GenerateCode(input, settings);

        // Assert
        actual.NormalizeLineEndings().Should().Be(@"/****** Object:  Table [dbo].[TestEntity] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TestEntity](
	[Id] int IDENTITY(1, 1) NOT NULL,
	[Name] varchar(32) NOT NULL,
	[Description] varchar(255) NOT NULL,
 CONSTRAINT [PK_TestEntity] PRIMARY KEY CLUSTERED
(
	[Id] ASC
) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  StoredProcedure [dbo].[InsertTestEntity] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[InsertTestEntity]
	@Name varchar(32),
	@Description varchar(255)
AS
BEGIN
    INSERT INTO [TestEntity]([Name], [Description]) OUTPUT INSERTED.[Id], INSERTED.[Name], INSERTED.[Description] VALUES(@Name, @Description)
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateTestEntity] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateTestEntity]
	@Name varchar(32),
	@Description varchar(255)
AS
BEGIN
    UPDATE [TestEntity] SET [Name] = @Name, [Description] = @Description OUTPUT INSERTED.[Id], INSERTED.[Name], INSERTED.[Description] WHERE [Id] = @IdOriginal AND [Name] = @NameOriginal AND [Description] = @DescriptionOriginal
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteTestEntity] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteTestEntity]
	@Name varchar(32),
	@Description varchar(255)
AS
BEGIN
    DELETE FROM [TestEntity] OUTPUT DELETED.[Id], DELETED.[Name], DELETED.[Description] WHERE [Id] = @IdOriginal AND [Name] = @NameOriginal AND [Description] = @DescriptionOriginal
END
GO
");
    }

    [Fact]
    public void Can_Generate_DatabaseSchema_With_All_Bells_And_Whistles()
    {
        // Arrange
        var settings = GeneratorSettings.Default;
        var input = new[]
        {
            new DataObjectInfoBuilder()
                .WithName("Test")
                .AddFields
                (
                    new FieldInfoBuilder().WithName("Id").WithSqlIdentity().WithType(typeof(int)),
                    new FieldInfoBuilder().WithName("Name").WithIsSqlRequired().WithType(typeof(string)).WithStringLength(50)
                )
                .AddAdditionalStoredProcedures(new StoredProcedureBuilder().WithName("usp_Custom").AddStatements(new LiteralSqlStatementBuilder().WithStatement("--Code goes here")))
                .AddViews(new ViewBuilder().WithName("VW_MyView").AddSelectFields(new ViewFieldBuilder().WithSourceObjectName("Field").WithName("MyField")).AddSources(new ViewSourceBuilder().WithName("MyTable")))
                .AddIndexes(new IndexBuilder().WithName("IX_MyIndex").AddFields(new IndexFieldBuilder().WithName("MyField")))
                .AddForeignKeyConstraints(new ForeignKeyConstraintBuilder()
                    .WithName("FK_MyFK")
                    .WithForeignTableName("ForeignTable")
                    .AddLocalFields(new ForeignKeyConstraintFieldBuilder().WithName("Name"))
                    .AddForeignFields(new ForeignKeyConstraintFieldBuilder().WithName("Name")))
                .AddCheckConstraints(new CheckConstraintBuilder().WithName("MyContraint").WithExpression("LEN(Name) > 1 AND LEN(Description) > 1"))
                .Build()
        }.ToSchemas(settings);

        // Act
        var actual = GenerateCode(input, settings);

        // Assert
        actual.NormalizeLineEndings().Should().Be(@"/****** Object:  Table [dbo].[Test] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Test](
	[Id] int IDENTITY(1, 1) NULL,
	[Name] varchar(50) NOT NULL,
    CONSTRAINT [MyContraint]
    CHECK (LEN(Name) > 1 AND LEN(Description) > 1)
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
CREATE NONCLUSTERED INDEX [IX_MyIndex] ON [dbo].[Test]
(
	[MyField] ASC
) ON [PRIMARY]
GO
/****** Object:  ForeignKey [FK_MyFK] ******/
ALTER TABLE [dbo].[Test]  WITH CHECK ADD  CONSTRAINT [FK_MyFK] FOREIGN KEY([Name])
REFERENCES [dbo].[ForeignTable] ([Name])
ON UPDATE NO ACTION
ON DELETE NO ACTION
GO
ALTER TABLE [dbo].[Test] CHECK CONSTRAINT [FK_MyFK]
GO
/****** Object:  StoredProcedure [dbo].[usp_Custom] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_Custom]
AS
BEGIN
    --Code goes here
END
GO
/****** Object:  View [dbo].[VW_MyView] ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VW_MyView]
AS
SELECT
    [Field]
FROM
    [MyTable]
GO
");
    }
}
