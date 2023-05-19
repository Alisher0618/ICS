// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;

namespace Tracker.DAL.Factories;
internal class DbContextSqLiteFactory : IDbContextFactory<TrackerDbContext>
{
    private readonly string _databaseName;
    private readonly bool _seedTestingData;

    public DbContextSqLiteFactory(string databaseName, bool seedTestingData = false)
    {
        _databaseName = databaseName;
        _seedTestingData = seedTestingData;
    }

    public TrackerDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<TrackerDbContext> builder = new();

        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");

        return new TrackerDbContext(builder.Options, _seedTestingData);
    }
}
