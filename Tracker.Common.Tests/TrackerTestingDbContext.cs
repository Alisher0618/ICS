// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.Common.Tests.Seeds;
using Tracker.DAL;

namespace Tracker.Common.Tests;

public class TrackerTestingDbContext : TrackerDbContext
{
    private readonly bool _seedTestingData;

    public TrackerTestingDbContext(DbContextOptions contextOptions, bool seedTestingData = false)
        : base(contextOptions, seedDemoData: false)
    {
        _seedTestingData = seedTestingData;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (_seedTestingData)
        {
            UserSeeds.Seed(modelBuilder);
            ActivitySeeds.Seed(modelBuilder);
            ProjectSeeds.Seed(modelBuilder);
        }
    }
}
