// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.DAL;

namespace Tracker.Common.Tests.Factories;

public class DbContextSQLiteTestingFactory : IDbContextFactory<TrackerDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextSQLiteTestingFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }
    public TrackerDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<TrackerDbContext> builder = new();
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");

        // contextOptionsBuilder.LogTo(System.Console.WriteLine); //Enable in case you want to see tests details, enabled may cause some inconsistencies in tests
        // builder.EnableSensitiveDataLogging();

        return new TrackerTestingDbContext(builder.Options, _seedTestingData);
    }
}
