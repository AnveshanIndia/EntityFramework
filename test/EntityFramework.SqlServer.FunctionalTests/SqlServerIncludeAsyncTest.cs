// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Data.Entity.FunctionalTests;

namespace Microsoft.Data.Entity.SqlServer.FunctionalTests
{
    public class SqlServerIncludeAsyncTest : IncludeAsyncTestBase<SqlServerNorthwindQueryFixture>
    {
        public SqlServerIncludeAsyncTest(SqlServerNorthwindQueryFixture fixture)
            : base(fixture)
        {
        }
    }
}
