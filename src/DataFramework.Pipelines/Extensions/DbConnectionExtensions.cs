using CrossCutting.Data.Sql.Extensions;
using DatabaseFramework.Domain.Domains;
using DataFramework.Domain.Builders;

namespace DataFramework.Pipelines.Extensions;

public static  class DbConnectionExtensions
{
    private static readonly string[] FixedSizeNumberDataTypes = new[] { "bit", "tinyint", "smallint", "int", "bigint" };

    /// <summary>
    /// Gets the data object infos.
    /// </summary>
    /// <param name="connection">The connection.</param>
    /// <param name="assemblyName">Name of the assembly.</param>
    /// <param name="typeNamePrefix">The type name prefix.</param>
    /// <param name="isVisible">if set to <c>true</c> [is visible].</param>
    /// <param name="isReadOnly">if set to <c>true</c> [is read only].</param>
    /// <param name="isQueryable">if set to <c>true</c> [is queryable].</param>
    /// <param name="includeTables">The include tables.</param>
    /// <param name="excludeTables">The exclude tables.</param>
    /// <param name="extractRelations">if set to <c>true</c> [extract relations].</param>
    /// <param name="addPropertiesForRelations">if set to <c>true</c> [add properties for relations].</param>
    /// <param name="canSet">if set to <c>true</c> the CanSet property of the fields will be set to true, which means property setters will be generated on code generation.</param>
    /// <param name="defaultSchemaName">Default name of the schema.</param>
    /// <returns></returns>
    public static IEnumerable<DataObjectInfo> GetDataObjectInfos
    (
        this IDbConnection connection,
        string assemblyName,
        string typeNamePrefix,
        bool isVisible = true,
        bool isReadOnly = false,
        bool isQueryable = true,
        string[] includeTables = null,
        string[] excludeTables = null,
        bool extractRelations = true,
        bool addPropertiesForRelations = false,
        bool canSet = true,
        string defaultSchemaName = "dbo"
    )
    {
        var dataObjectInfos = new List<DataObjectInfoBuilder>();
        var relationFields = new Collection<string>();

        using (var cmd = connection.CreateCommand())
        {
            cmd.CommandText = "SELECT TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME, ORDINAL_POSITION, COLUMN_DEFAULT, IS_NULLABLE, DATA_TYPE, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_SCALE, COLUMNPROPERTY(object_id(TABLE_NAME), COLUMN_NAME, 'IsIdentity') AS IsIdentity, COLUMNPROPERTY(object_id(TABLE_NAME), COLUMN_NAME, 'IsComputed') AS IsComputed, COLLATION_NAME, CASE WHEN v.object_id IS NULL THEN CAST(0 as bit) ELSE CAST(1 as bit) END AS IsView, m.definition AS ViewDefinition FROM INFORMATION_SCHEMA.COLUMNS c LEFT JOIN sys.views v ON object_id(c.TABLE_NAME) = v.object_id LEFT JOIN sys.sql_modules m ON v.object_id = m.object_id WHERE TABLE_SCHEMA + '.' + TABLE_NAME <> 'dbo.sysdiagrams'";
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var tableName = reader.GetString("TABLE_NAME");

                    if (excludeTables?.Any(s => s.Equals(tableName, StringComparison.OrdinalIgnoreCase)) == true)
                    {
                        continue;
                    }

                    if (includeTables?.Any(s => s.Equals(tableName, StringComparison.OrdinalIgnoreCase)) == false)
                    {
                        continue;
                    }

                    var columnName = reader.GetString("COLUMN_NAME");

                    if (extractRelations && relationFields.Any(s => s.Equals(tableName + "." + columnName, StringComparison.OrdinalIgnoreCase)))
                    {
                        continue;
                    }

                    var dataObjectInfo = dataObjectInfos.Find(doi => doi.DatabaseTableName.WhenNullOrEmpty(doi.Name).Equals(tableName, StringComparison.OrdinalIgnoreCase));

                    if (dataObjectInfo is null)
                    {
                        var schemaName = reader.GetString("TABLE_SCHEMA");
                        //TODO: Review if we want to store the 'CatalogName', which is the database name
                        //var catalogName = reader.GetString("TABLE_CATALOG");
                        var isView = reader.GetBoolean("IsView");
                        var viewDefinition = reader.GetString("ViewDefinition", string.Empty);
                        dataObjectInfo = new DataObjectInfoBuilder()
                            .WithAssemblyName(assemblyName)
                            .WithTypeName(string.IsNullOrEmpty(typeNamePrefix)
                                ? string.Empty
                                : typeNamePrefix + "." + tableName)
                            .WithDatabaseTableName(tableName)
                            .WithIsVisible(isVisible)
                            .WithIsReadOnly(isReadOnly || isView)
                            .WithIsQueryable(isQueryable)
                            .WithViewDefinition(viewDefinition)
                            .WithDatabaseSchemaName(schemaName)
                            //TODO: Review if we want to store the 'CatalogName', which is the database name
                            //.WithDatabaseCatalogName(catalogName)
                            ;

                        dataObjectInfos.Add(dataObjectInfo);
                    }

                    var field = new FieldInfoBuilder();

                    if (!reader.IsDBNull("CHARACTER_MAXIMUM_LENGTH"))
                    {
                        var maxLength = reader.GetInt32("CHARACTER_MAXIMUM_LENGTH");
                        if (maxLength == -1)
                        {
                            field.WithIsMaxLengthString(true);
                        }
                        else
                        {
                            field.WithStringMaxLength(maxLength);
                        }
                    }

                    var dataType = reader.GetString("DATA_TYPE");
                    var dataTypeIsFixedSizeNumber = FixedSizeNumberDataTypes.Contains(dataType, StringComparer.OrdinalIgnoreCase);

                    if (!dataTypeIsFixedSizeNumber)
                    {
                        if (!reader.IsDBNull("NUMERIC_PRECISION"))
                        {
                            field.WithDatabaseNumericPrecision(reader.GetByte("NUMERIC_PRECISION"));
                        }
                        if (!reader.IsDBNull("NUMERIC_SCALE"))
                        {
                            field.WithDatabaseNumericScale(Convert.ToByte(reader.GetInt32("NUMERIC_SCALE")));
                        }
                    }

                    //Add SqlDataType attribute
                    if (!reader.IsDBNull("NUMERIC_PRECISION") && !reader.IsDBNull("NUMERIC_SCALE") && !dataTypeIsFixedSizeNumber)
                    {
                        field.WithDatabaseFieldType(dataType + "(" + reader.GetByte("NUMERIC_PRECISION").ToString() + "," + reader.GetInt32("NUMERIC_SCALE").ToString() + ")");
                    }
                    else if (!reader.IsDBNull("CHARACTER_MAXIMUM_LENGTH"))
                    {
                        var len = reader.GetInt32("CHARACTER_MAXIMUM_LENGTH").ToString();
                        if (len == "-1")
                        {
                            len = "max";
                        }
                        field.WithDatabaseFieldType(dataType + "(" + len + ")");
                    }
                    else
                    {
                        field.WithDatabaseFieldType(dataType);
                    }

                    if (reader.GetInt32("IsIdentity") == 1)
                    {
                        field.WithIsDatabaseIdentityField();
                    }

                    if (dataType == "rowversion")
                    {
                        field.WithIsRowVersion();
                    }

                    var useForCheckOnOriginalValues = dataType == "rowversion" || dataType == "timestamp";

                    field
                        .WithName(columnName)
                        .WithTypeName(MapSqlType(dataType, reader.GetString("IS_NULLABLE")))
                        .WithIsVisible(isVisible)
                        .WithIsReadOnly(dataObjectInfo.IsReadOnly || reader.GetInt32("IsIdentity") == 1)
                        .WithIsIdentityField(reader.GetInt32("IsIdentity") == 1)
                        //TODO: Review whether to set IsComputed to true for IsIdentity fields. Currently needed because Identity fields are only available on SqlServer level.
                        .WithIsComputed(reader.GetInt32("IsIdentity") == 1 || reader.GetInt32("IsComputed") == 1)
                        .WithDatabaseStringCollation(reader.GetNullableString("COLLATION_NAME"))
                        .WithCanSet(canSet)
                        .WithUseForConcurrencyCheck(useForCheckOnOriginalValues)
                        .WithDefaultValue(reader.GetValue<object?>("COLUMN_DEFAULT", null));

                    dataObjectInfo.AddFields(field);
                }
            }
        }

        //connection.AddKeys(dataObjectInfos, extractRelations, defaultSchemaName);

        //if (extractRelations)
        //{
        //    var newDois = new List<DataObjectInfoBuilder>();
        //    foreach (var doi in dataObjectInfos)
        //    {
        //        newDois.Add(doi.AddRelations(typeNamePrefix, true, addPropertiesForRelations, dataObjectInfos));
        //    }
        //    dataObjectInfos = newDois;
        //}

        return dataObjectInfos.Select(x => x.Build());
    }

 //   /// <summary>
 //   /// Adds the relations.
 //   /// </summary>
 //   /// <param name="instance">The instance.</param>
 //   /// <param name="typeNamePrefix">The type name prefix.</param>
 //   /// <param name="addReverseRelations">if set to <c>true</c> [add reverse relations].</param>
 //   /// <param name="addPropertiesForRelations">if set to <c>true</c> [add properties for relations].</param>
 //   /// <param name="knownDataObjectInfos">The known data object infos.</param>
 //   public static DataObjectInfoBuilder AddRelations
 //   (
 //       this DataObjectInfoBuilder instance,
 //       string typeNamePrefix,
 //       bool addReverseRelations = false,
 //       bool addPropertiesForRelations = false,
 //       IEnumerable<DataObjectInfoBuilder>? knownDataObjectInfos = null
 //   )
 //   {
 //       foreach (var metadata in instance.Metadata.Where(md => md.Name == RelationAttribute.MetadataKey).ToArray())
 //       {
 //           var relation = RelationAttribute.ParseFromMetadataValue(metadata.GetValueWithContext(instance).ToStringWithNullCheck());
 //           if (relation == null)
 //           {
 //               continue; //something went wrong
 //           }

 //           if (relation.IsReverse)
 //           {
 //               continue; //already added from the other side
 //           }

 //           var relationDataObjectInfo = knownDataObjectInfos?.FirstOrDefault(doi => doi.DatabaseTableName.WhenNullOrEmpty(doi.Name) == /*relation.ForeignTableName*/ relation.LocalTableName);

 //           if (relationDataObjectInfo != null)
 //           {
 //               //note we're always adding a single field. a relation is always 'one' on this end on sql server. (parent side)
 //               if (addPropertiesForRelations)
 //               {
 //                   relationDataObjectInfo = relationDataObjectInfo.With
 //                   (
 //                       new Common.Default.FieldInfo
 //                       (
 //                           relation.PropertyName ?? instance.DatabaseTableName.WhenNullOrEmpty(instance.Name),
 //                           relation.IsMultiple
 //                               ? GenerateCollectionTypeName(typeNamePrefix, instance.Name)
 //                               : string.Format("{0}.{1}", typeNamePrefix, instance.Name)
 //                       )
 //                   );
 //               }

 //               relationDataObjectInfo = relationDataObjectInfo
 //                   .With(relation.Reverse().ToMetadata()) //also copy the relation to the other entity
 //                   .With
 //                   (
 //                       new Common.Attributes.HasAttribute
 //                       (
 //                           instance.TypeName,
 //                           Common.Attributes.Multiplicity.Zero,
 //                           Common.Attributes.Multiplicity.Many,
 //                           relation.PropertyName
 //                       ).ToMetadata()
 //                   );
 //           }

 //           if (!addReverseRelations)
 //           {
 //               continue;
 //           }

 //           //child side
 //           var relationDataObjectInfoFields = instance.Fields.Where(fieldInfo => relation.ForeignFieldNames.Contains(fieldInfo.GetFieldName(context: instance)));
 //           if (relationDataObjectInfo != null)
 //           {
 //               instance = instance.With
 //               (
 //                   new Common.Attributes.HasAttribute
 //                   (
 //                       relationDataObjectInfo.TypeName,
 //                       relationDataObjectInfoFields.All(fld => fld.GetSqlIsRequired(context: instance) || fld.GetIsRequired(context: instance))
 //                           ? Common.Attributes.Multiplicity.One
 //                           : Common.Attributes.Multiplicity.Zero,
 //                       Common.Attributes.Multiplicity.One,
 //                       relation.ForeignPropertyName
 //                   ).ToMetadata()
 //               );
 //           }

 //           if (addPropertiesForRelations)
 //           {
 //               instance = instance.With
 //               (
 //                   new Common.Default.FieldInfo
 //                   (
 //                       relation.ForeignPropertyName ?? relation.ForeignTableName,
 //                       typeNamePrefix + "." + relation.LocalTableName
 //                   )
 //               );
 //           }
 //       }

 //       return instance;
 //   }

 //   /// <summary>
 //   /// Adds primary and foreign key information to metadata of DataObjectInfo and child IFieldInfo instances.
 //   /// </summary>
 //   /// <param name="connection">The connection.</param>
 //   /// <param name="dataObjectInfos">The data object infos.</param>
 //   /// <param name="extractRelations">if set to <c>true</c> [extract relations].</param>
 //   /// <param name="defaultSchemaName">Default name of the schema.</param>
 //   /// <exception cref="NotSupportedException">Can't update field because it's not of type Common.Default.FieldInfo</exception>
 //   private static void AddKeys
 //   (
 //       this IDbConnection connection,
 //       IEnumerable<DataObjectInfoBuilder> dataObjectInfos,
 //       bool extractRelations,
 //       string defaultSchemaName = "dbo"
 //   )
 //   {
 //       using (var cmd = connection.CreateCommand())
 //       {
 //           cmd.CommandText = @"SELECT
 //  schema_name(ta.schema_id) AS [SchemaName]
 // ,ta.name AS [TableName]
 // ,d.name AS [FileGroupName]
 // ,ind.name
 // ,indcol.key_ordinal AS [Ord]
 // ,indcol.is_descending_key
 // ,col.name As [ColumnName]
 // ,ind.type_desc
 // ,ind.fill_factor
 // ,ind.is_primary_key
 // ,ind.is_unique
 // ,ind.is_unique_constraint
 //FROM sys.tables ta
 // INNER JOIN sys.indexes ind
 //  ON ind.object_id = ta.object_id
 // inner join sys.index_columns indcol
 //  ON indcol.object_id = ta.object_id
 //   and indcol.index_id = ind.index_id
 // inner join sys.columns col
 //  ON col.object_id = ta.object_id
 //   AND col.column_id = indcol.column_id
 // INNER JOIN sys.filegroups d
 //  ON ind.data_space_id = d.data_space_id
 //ORDER BY
 //  ta.name
 // ,indcol.key_ordinal";
 //           using (var reader = cmd.ExecuteReader())
 //           {
 //               var indexes = new Dictionary<string, int>();
 //               while (reader.Read())
 //               {
 //                   var schemaName = reader.GetString("SchemaName");
 //                   var tableName = reader.GetString("TableName");
 //                   var entity = dataObjectInfos.FirstOrDefault(doi => doi.DatabaseSchemaName/*.WhenNullOrEmpty("dbo")*/.WhenNullOrEmpty(defaultSchemaName).Equals(schemaName.WhenNullOrEmpty(defaultSchemaName), StringComparison.OrdinalIgnoreCase) && doi.GetDatabaseTableName().Equals(tableName, StringComparison.OrdinalIgnoreCase));

 //                   if (entity == null)
 //                   {
 //                       continue;
 //                   }

 //                   var fieldName = reader.GetString("ColumnName");
 //                   //TODO: Refactor this method to use builder, so we can modify data...
 //                   var field = entity.Fields.FirstOrDefault(f => f.GetDatabaseFieldName() == fieldName);

 //                   if (field == null)
 //                   {
 //                       continue;
 //                   }

 //                   int tableIndex = 0;

 //                   if (!indexes.ContainsKey(schemaName + "." + tableName))
 //                   {
 //                       indexes.Add(schemaName + "." + tableName, tableIndex);
 //                   }
 //                   else
 //                   {
 //                       tableIndex = indexes[schemaName + "." + tableName] + 1;
 //                       indexes[schemaName + "." + tableName] = tableIndex;
 //                   }

 //                   var indexName = reader.GetString("name");
 //                   var fileGroupName = reader.GetString("FileGroupName");

 //                   if (reader.GetBoolean("is_primary_key"))
 //                   {
 //                       field.With(new PrimaryKeyAttribute(true, tableIndex, indexName, fileGroupName).ToMetadata());
 //                   }
 //                   else if (reader.GetBoolean("is_unique_constraint"))
 //                   {
 //                       //TODO: Add support for unique constraints
 //                   }
 //                   else
 //                   {
 //                       //index
 //                       field.With(new IndexAttribute(true, tableIndex, reader.GetBoolean("is_unique"), reader.GetBoolean("is_descending_key"), indexName, fileGroupName).ToMetadata());
 //                   }
 //               }
 //           }
 //       }

 //       if (extractRelations)
 //       {
 //           using (var cmd = connection.CreateCommand())
 //           {
 //               cmd.CommandText = @"SELECT CAST(f.name AS varchar(255)) AS foreign_key_name
 //   , CAST(c.name AS varchar(255)) AS foreign_table
 //   , CAST(fc.name AS varchar(255)) AS foreign_column_1
 //   , CAST(fc2.name AS varchar(255)) AS foreign_column_2
 //   , CAST(fc3.name AS varchar(255)) AS foreign_column_3
 //   , CAST(fc4.name AS varchar(255)) AS foreign_column_4
 //   , CAST(p.name AS varchar(255)) AS primary_table
 //   , CAST(rc.name AS varchar(255)) AS primary_column_1
 //   , CAST(rc2.name AS varchar(255)) AS primary_column_2
 //   , CAST(rc3.name AS varchar(255)) AS primary_column_3
 //   , CAST(rc4.name AS varchar(255)) AS primary_column_4
 //   , fk.delete_referential_action_desc
 //   , fk.update_referential_action_desc
 //   FROM sysobjects f
 //   INNER JOIN sysobjects c ON f.parent_obj = c.id
 //   INNER JOIN sysreferences r ON f.id =  r.constid
 //   INNER JOIN sysobjects p ON r.rkeyid = p.id
 //   INNER JOIN sys.foreign_keys fk ON f.id = fk.object_id
 //   INNER JOIN syscolumns rc ON r.rkeyid = rc.id AND r.rkey1 = rc.colid
 //   INNER JOIN syscolumns fc ON r.fkeyid = fc.id AND r.fkey1 = fc.colid
 //   LEFT JOIN syscolumns rc2 ON r.rkeyid = rc2.id AND r.rkey2 = rc2.colid
 //   LEFT JOIN syscolumns fc2 ON r.fkeyid = fc2.id AND r.fkey2 = fc2.colid
 //   LEFT JOIN syscolumns rc3 ON r.rkeyid = rc3.id AND r.rkey3 = rc3.colid
 //   LEFT JOIN syscolumns fc3 ON r.fkeyid = fc3.id AND r.fkey3 = fc3.colid
 //   LEFT JOIN syscolumns rc4 ON r.rkeyid = rc4.id AND r.rkey4 = rc4.colid
 //   LEFT JOIN syscolumns fc4 ON r.fkeyid = fc4.id AND r.fkey4 = fc4.colid
 //   WHERE f.type = 'F'
 //ORDER BY cast(p.name as varchar(255))";

 //               using (var reader = cmd.ExecuteReader())
 //               {
 //                   var relations = reader.FindMany(_ => new
 //                   {
 //                       foreign_key_name = reader.GetString("foreign_key_name"),
 //                       foreign_table = reader.GetString("foreign_table"),
 //                       foreign_column_1 = reader.GetString("foreign_column_1"),
 //                       foreign_column_2 = reader.GetString("foreign_column_2", null),
 //                       foreign_column_3 = reader.GetString("foreign_column_3", null),
 //                       foreign_column_4 = reader.GetString("foreign_column_4", null),
 //                       primary_table = reader.GetString("primary_table"),
 //                       primary_column_1 = reader.GetString("primary_column_1"),
 //                       primary_column_2 = reader.GetString("primary_column_2", null),
 //                       primary_column_3 = reader.GetString("primary_column_3", null),
 //                       primary_column_4 = reader.GetString("primary_column_4", null),
 //                       delete_referential_action_desc = reader.GetString("delete_referential_action_desc"), //NO_ACTION, CASCADE, SET_NULL, SET_DEFAULT
 //                       update_referential_action_desc = reader.GetString("update_referential_action_desc"), //NO_ACTION, CASCADE, SET_NULL, SET_DEFAULT
 //                   }).ToArray();

 //                   foreach (var relation in relations)
 //                   {
 //                       var parentEntity = dataObjectInfos.FirstOrDefault(doi => doi.DatabaseTableName.WhenNullOrEmpty(doi.Name).Equals(relation.primary_table, StringComparison.OrdinalIgnoreCase));

 //                       if (parentEntity == null)
 //                       {
 //                           continue;
 //                       }

 //                       var parentColumn1 = parentEntity.Fields.FirstOrDefault(f => f.DatabaseFieldName.WhenNullOrEmpty(f.Name) == relation.primary_column_1);

 //                       if (parentColumn1 == null)
 //                       {
 //                           continue;
 //                       }

 //                       var parentColumn2 = parentEntity.Fields.FirstOrDefault(f => f.DatabaseFieldName.WhenNullOrEmpty(f.Name) == relation.primary_column_2);
 //                       var parentColumn3 = parentEntity.Fields.FirstOrDefault(f => f.DatabaseFieldName.WhenNullOrEmpty(f.Name) == relation.primary_column_3);
 //                       var parentColumn4 = parentEntity.Fields.FirstOrDefault(f => f.DatabaseFieldName.WhenNullOrEmpty(f.Name) == relation.primary_column_4);
 //                       var referencedEntity = dataObjectInfos.FirstOrDefault(doi => doi.DatabaseTableName.WhenNullOrEmpty(doi.Name).Equals(relation.foreign_table, StringComparison.OrdinalIgnoreCase));

 //                       if (referencedEntity == null)
 //                       {
 //                           continue;
 //                       }

 //                       //note that we use sql column names on both ends
 //                       var localFieldNames = new List<string>
 //                           {
 //                               relation.primary_column_1
 //                           };

 //                       if (!string.IsNullOrEmpty(relation.primary_column_2)) localFieldNames.Add(relation.primary_column_2);
 //                       if (!string.IsNullOrEmpty(relation.primary_column_3)) localFieldNames.Add(relation.primary_column_3);
 //                       if (!string.IsNullOrEmpty(relation.primary_column_4)) localFieldNames.Add(relation.primary_column_4);
 //                       var foreignFieldNames = new List<string>
 //                           {
 //                               relation.foreign_column_1
 //                           };
 //                       if (!string.IsNullOrEmpty(relation.foreign_column_2)) foreignFieldNames.Add(relation.foreign_column_2);
 //                       if (!string.IsNullOrEmpty(relation.foreign_column_3)) foreignFieldNames.Add(relation.foreign_column_3);
 //                       if (!string.IsNullOrEmpty(relation.foreign_column_4)) foreignFieldNames.Add(relation.foreign_column_4);
 //                       referencedEntity/*parentEntity*/.With
 //                       (
 //                           new RelationAttribute
 //                           (
 //                               relation.primary_table,
 //                               relation.foreign_table,
 //                               localFieldNames.ToArray(),
 //                               foreignFieldNames.ToArray(),
 //                               true, //TODO: Review value for IsMultiple
 //                               relation.foreign_table, //TODO: Review value for PropertyName
 //                               relation.foreign_key_name,
 //                               relation.primary_table, //TODO: Review value for RemotePropertyName
 //                               GetCascadeAction(relation.update_referential_action_desc),
 //                               GetCascadeAction(relation.delete_referential_action_desc)
 //                           ).ToMetadata()
 //                       );
 //                   }
 //               }
 //           }
 //       }
 //   }

    private static CascadeAction GetCascadeAction(string value)
    {
        //NO_ACTION, CASCADE, SET_NULL, SET_DEFAULT
        return value switch
        {
            "NO_ACTION" => CascadeAction.NoAction,
            "CASCADE" => CascadeAction.Cascade,
            "SET_NULL" => CascadeAction.SetNull,
            "SET_DEFAULT" => CascadeAction.SetDefault,
            _ => throw new ArgumentOutOfRangeException(nameof(value), "Value [" + value + "] is not a valid CascadeAction"),
        };
    }

    /// <summary>
    /// Maps the Sql type to Clr type.
    /// </summary>
    /// <param name="sqlTypeName">Name of the SQL type.</param>
    /// <param name="isNullable">The is nullable.</param>
    /// <returns>
    /// CLR typename.
    /// </returns>
    private static string MapSqlType(string sqlTypeName, string isNullable)
    {
        var nullable = isNullable == "YES";

        return sqlTypeName switch
        {
            //TODO: Verify other types like xml, sql_variant
            "bit" => nullable
                ? typeof(bool?).FullName
                : typeof(bool).FullName,
            "tinyint" => nullable
                ? typeof(byte?).FullName
                : typeof(byte).FullName,
            "int" => nullable
                ? typeof(int?).FullName
                : typeof(int).FullName,
            "smallint" => nullable
                ? typeof(short?).FullName
                : typeof(short).FullName,
            "bigint" => nullable
                ? typeof(long?).FullName
                : typeof(long).FullName,
            "uniqueidentifier" => nullable
                ? typeof(Guid?).FullName
                : typeof(Guid).FullName,
            "binary" or "timestamp" or "rowversion" or "image" or "varbinary" => typeof(byte[]).FullName,
            "date" or "datetime" or "datetime2" or "smalldatetime" => nullable
                ? typeof(DateTime?).FullName
                : typeof(DateTime).FullName,
            "datetimeoffset" => nullable
                ? typeof(DateTimeOffset?).FullName
                : typeof(DateTimeOffset).FullName,
            "time" => nullable
                ? typeof(TimeSpan?).FullName
                : typeof(TimeSpan).FullName,
            "decimal" or "money" or "numeric" or "smallmoney" => nullable
                ? typeof(decimal?).FullName
                : typeof(decimal).FullName,
            "float" => nullable
                ? typeof(double?).FullName
                : typeof(double).FullName,
            "real" => nullable
                ? typeof(float?).FullName
                : typeof(float).FullName,
            "char" or "nchar" or "varchar" or "nvarchar" or "text" or "ntext" => typeof(string).FullName,
            _ => throw new ArgumentOutOfRangeException(nameof(sqlTypeName), "Unsupported sql type: " + sqlTypeName),
        };
    }

    private static string GenerateCollectionTypeName(string typeNamePrefix, string typeName)
        => typeof(ICollection<string>)
            .FullName
            .Replace(typeof(string).AssemblyQualifiedName, typeNamePrefix + "." + typeName);
}
