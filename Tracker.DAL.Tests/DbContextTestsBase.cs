// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using CookBook.Common.Tests;
using Microsoft.EntityFrameworkCore;
using Tracker.Common.Tests.Factories;
using Xunit;
using Xunit.Abstractions;

namespace Tracker.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
        // DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);
        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        TrackerDbContextSUT = DbContextFactory.CreateDbContext();
    }

    protected IDbContextFactory<TrackerDbContext> DbContextFactory { get; }
    protected TrackerDbContext TrackerDbContextSUT { get; }


    public async Task InitializeAsync()
    {
        await TrackerDbContextSUT.Database.EnsureDeletedAsync();
        await TrackerDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await TrackerDbContextSUT.Database.EnsureDeletedAsync();
        await TrackerDbContextSUT.DisposeAsync();
    }
}
