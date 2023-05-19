// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.Common.Enums;
using Tracker.Common.Tests.Seeds;
using Tracker.DAL.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Tracker.DAL.Tests;
public class DbContextActivityTests : DbContextTestsBase
{
    public DbContextActivityTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_Activity_Persisted()
    {
        //Arrange
        ActivityEntity entity = new()
        {
            Id = Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
            Name = string.Empty,
            Start = default,
            End = default,
            Type = ActivityType.Programming
        };

        //Act
        TrackerDbContextSUT.Activities.Add(entity);
        await TrackerDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        ActivityEntity actualEntities = await dbx.Activities.SingleAsync(i => i.Id == entity.Id);
        Assert.Equal(entity, actualEntities);
    }

    [Fact]
    public async Task GetAll_Activities_ContainsSeededProgramming()
    {
        //Act
        var entities = await TrackerDbContextSUT.Activities.ToArrayAsync();

        //Assert
        Assert.Contains(ActivitySeeds.Programming, entities);
    }

    [Fact]
    public async Task GetById_Activity_ProgrammingRetrieved()
    {
        //Act
        var entity = await TrackerDbContextSUT.Activities.SingleAsync(i => i.Id == ActivitySeeds.Programming.Id);

        //Assert
        Assert.Equal(ActivitySeeds.Programming, entity);
    }

    [Fact]
    public async Task Update_Activity_Persisted()
    {
        //Arrange
        var baseEntity = ActivitySeeds.TestingUpdate;
        var entity =
            baseEntity with
            {
                Start = new DateTime(1984, 1, 1),
                End = new DateTime(1984, 1, 1),
            };

        //Act
        TrackerDbContextSUT.Activities.Update(entity);
        await TrackerDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Activities.SingleAsync(i => i.Id == entity.Id);
        Assert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task Delete_Activity_TestingDeleted()
    {
        //Arrange
        var entityBase = ActivitySeeds.TestingDelete;

        //Act
        TrackerDbContextSUT.Activities.Remove(entityBase);
        await TrackerDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await TrackerDbContextSUT.Activities.AnyAsync(i => i.Id == entityBase.Id));
    }

    [Fact]
    public async Task DeleteById_Activity_TestingDeleted()
    {
        //Arrange
        var entityBase = ActivitySeeds.TestingDelete;

        //Act
        TrackerDbContextSUT.Remove(
            TrackerDbContextSUT.Activities.Single(i => i.Id == entityBase.Id));
        await TrackerDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await TrackerDbContextSUT.Activities.AnyAsync(i => i.Id == entityBase.Id));
    }
}
