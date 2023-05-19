// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Tracker.DAL.Factories;
public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<TrackerDbContext>
{
    public TrackerDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<TrackerDbContext> builder = new();

        // // Use for LocalDB migrations
        // builder.UseSqlServer(
        //     @"Data Source=(LocalDB)\MSSQLLocalDB;
        //         Initial Catalog = CookBook;
        //         MultipleActiveResultSets = True;
        //         Integrated Security = True;
        //         Encrypt = False;
        //         TrustServerCertificate = True;");

        builder.UseSqlite($"Data Source=Tracker;Cache=Shared");

        return new TrackerDbContext(builder.Options);
    }
}
