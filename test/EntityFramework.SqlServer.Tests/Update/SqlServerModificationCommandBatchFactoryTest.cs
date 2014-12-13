// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Update;
using Microsoft.Data.Entity.SqlServer.Update;
using Microsoft.Framework.ConfigurationModel;
using Xunit;
using Microsoft.Data.Entity.Infrastructure;

namespace Microsoft.Data.Entity.SqlServer.Tests.Update
{
    public class SqlServerModificationCommandBatchFactoryTest
    {
        [Fact]
        public void Uses_MaxBatchSize_specified_in_dbContextOptions()
        {
            var factory = new SqlServerModificationCommandBatchFactory(new SqlServerSqlGenerator());
            IDbContextOptions options = new DbContextOptions();
            options.RawOptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { { "SqlServer:MaxBatchSize", "1" } };

            var batch = factory.Create(options);

            Assert.True(factory.AddCommand(batch, new ModificationCommand(new SchemaQualifiedName("T1"), new ParameterNameGenerator(), p => p.SqlServer())));
            Assert.False(factory.AddCommand(batch, new ModificationCommand(new SchemaQualifiedName("T1"), new ParameterNameGenerator(), p => p.SqlServer())));
        }

        [Fact]
        public void MaxBatchSize_is_optional()
        {
            var factory = new SqlServerModificationCommandBatchFactory(new SqlServerSqlGenerator());
            IDbContextOptions options = new DbContextOptions();
            options.RawOptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            var batch = factory.Create(options);
            Assert.True(factory.AddCommand(batch, new ModificationCommand(new SchemaQualifiedName("T1"), new ParameterNameGenerator(), p => p.SqlServer())));
            Assert.True(factory.AddCommand(batch, new ModificationCommand(new SchemaQualifiedName("T1"), new ParameterNameGenerator(), p => p.SqlServer())));
        }

        [Fact]
        public void Throws_on_invalid_MaxBatchSize_specified_in_dbContextOptions()
        {
            var factory = new SqlServerModificationCommandBatchFactory(new SqlServerSqlGenerator());
            IDbContextOptions options = new DbContextOptions();
            options.RawOptions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) { { "SqlServer:MaxBatchSize", "one" } };

            Assert.Equal(Strings.IntegerConfigurationValueFormatError("SqlServer:MaxBatchSize", "one"),
                Assert.Throws<InvalidOperationException>(() => factory.Create(options)).Message);
        }
    }
}
