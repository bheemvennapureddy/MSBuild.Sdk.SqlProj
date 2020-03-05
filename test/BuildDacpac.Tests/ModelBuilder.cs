﻿using System.IO;
using System.Linq;
using Microsoft.SqlServer.Dac;
using Microsoft.SqlServer.Dac.Model;

namespace MSBuild.Sdk.SqlProj.BuildDacpac.Tests
{
    internal class ModelBuilder
    {
        private TSqlModel sqlModel;
        
        public ModelBuilder()
        {
            sqlModel = new TSqlModel(SqlServerVersion.Sql110, new TSqlModelOptions
            {
                AnsiNullsOn = true,
                Collation = "SQL_Latin1_General_CP1_CI_AI",
                CompatibilityLevel = 110,
                QuotedIdentifierOn = true,
            });
        }

        public ModelBuilder AddTable(string tableName, params (string name, string type)[] columns)
        {
            var columnsDefinition = string.Join(",", columns.Select(column => $"{column.name} {column.type}"));
            var tableDefinition = $"CREATE TABLE [{tableName}] ({columnsDefinition});";
            sqlModel.AddObjects(tableDefinition);
            return this;
        }

        public ModelBuilder AddStoredProcedure(string procName, string body)
        {
            var procDefinition = $"CREATE PROCEDURE [{procName}] AS BEGIN {body} END";
            sqlModel.AddObjects(procDefinition);
            return this;
        }

        public ModelBuilder AddReference(string path)
        {
            sqlModel.AddReference(path);
            return this;
        }

        public TSqlModel Build()
        {
            return sqlModel;
        }

        public string SaveAsPackage()
        {
            var filename = Path.GetTempFileName();
            DacPackageExtensions.BuildPackage(filename, sqlModel, new PackageMetadata());
            return filename;
        }
    }
}
