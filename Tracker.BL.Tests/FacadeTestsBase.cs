// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//
using CookBook.Common.Tests;
using Microsoft.EntityFrameworkCore;
using Tracker.BL.Mappers;
using Tracker.BL.Mappers.Interfaces;
using Tracker.Common.Tests.Factories;
using Tracker.DAL;
using Tracker.DAL.Mappers;
using Tracker.DAL.UnitOfWork;
using Xunit;
using Xunit.Abstractions;

namespace Tracker.BL.Tests;

public class FacadeTestsBase : IAsyncLifetime
{
    protected FacadeTestsBase(ITestOutputHelper output)
    {
        XUnitTestOutputConverter converter = new(output);
        Console.SetOut(converter);

        // DbContextFactory = new DbContextTestingInMemoryFactory(GetType().Name, seedTestingData: true);
        // DbContextFactory = new DbContextLocalDBTestingFactory(GetType().FullName!, seedTestingData: true);
        ActivityEntityMapper = new ActivityEntityMapper();
        ActivityProjectEntityMapper = new ActivityProjectEntityMapper();
        ProjectEntityMapper = new ProjectEntityMapper();
        ProjectUserEntityMapper = new ProjectUserEntityMapper();
        UserEntityMapper = new UserEntityMapper();

        ActivityModelMapper = new ActivityModelMapper();
        UserModelMapper = new UserModelMapper(ActivityModelMapper);
        ProjectModelMapper = new ProjectModelMapper(ActivityModelMapper, UserModelMapper);

        DbContextFactory = new DbContextSQLiteTestingFactory(GetType().FullName!, seedTestingData: true);

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<TrackerDbContext> DbContextFactory { get; }

    protected ActivityEntityMapper ActivityEntityMapper { get; }
    protected ActivityProjectEntityMapper ActivityProjectEntityMapper { get; }
    protected ProjectEntityMapper ProjectEntityMapper { get; }
    protected ProjectUserEntityMapper ProjectUserEntityMapper { get; }
    protected UserEntityMapper UserEntityMapper { get; }

    protected IActivityModelMapper ActivityModelMapper { get; }
    protected ProjectModelMapper ProjectModelMapper { get; }
    protected IUserModelMapper UserModelMapper { get; }
    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
        await dbx.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        await dbx.Database.EnsureDeletedAsync();
    }
}
