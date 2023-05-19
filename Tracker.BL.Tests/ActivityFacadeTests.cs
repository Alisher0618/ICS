// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.BL.Facades;
using Tracker.BL.Facades.Interfaces;
using Tracker.BL.Models;
using Tracker.Common.Enums;
using Tracker.Common.Tests;
using Tracker.Common.Tests.Seeds;
using Xunit;
using Xunit.Abstractions;

namespace Tracker.BL.Tests;

public sealed class ActivityFacadeTests : FacadeTestsBase
{
    private readonly IActivityFacade _activityFacadeSUT;

    public ActivityFacadeTests(ITestOutputHelper output) : base(output)
    {
        _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        var model = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = @"nevim",
            Start = new DateTime(2023, 4, 8),
            End = new DateTime(2023, 4, 10),
            Type = ActivityType.Testing,
            Description = @"nevim",

        };

        var _ = await _activityFacadeSUT.SaveAsync(model);
    }

    [Fact]
    public async Task GetAll_Single_SeededActivity()
    {
        var activities = await _activityFacadeSUT.GetAsync();
        var activity = activities.Single(i => i.Id == ActivitySeeds.TestingUpdate.Id);

        DeepAssert.Equal(ActivityModelMapper.MapToListModel(ActivitySeeds.TestingUpdate), activity);
    }

    [Fact]
    public async Task GetById_SeededActivity()
    {
        var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.Testing.Id);

        DeepAssert.Equal(ActivityModelMapper.MapToDetailModel(ActivitySeeds.Testing), activity);
    }

    [Fact]
    public async Task GetById_NonExistent()
    {
        var activity = await _activityFacadeSUT.GetAsync(ActivitySeeds.EmptyActivity.Id);

        Assert.Null(activity);
    }

    [Fact]
    public async Task SeededActivity_DeleteById_Deleted()
    {
        await _activityFacadeSUT.DeleteAsync(ActivitySeeds.Testing.Id);

        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Activities.AnyAsync(i => i.Id == ActivitySeeds.Testing.Id));
    }

    [Fact]
    public async Task NewActivity_InsertOrUpdate_ActivityAdded()
    {
        //Arrange
        var activity = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            Name = @"nevim",
            Start = new DateTime(2023, 4, 8),
            End = new DateTime(2023, 4, 10),
            Type = ActivityType.Testing,
            Description = @"nevim",
        };

        //Act
        activity = await _activityFacadeSUT.SaveAsync(activity);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var activityFromDb = await dbxAssert.Activities.SingleAsync(i => i.Id == activity.Id);
        DeepAssert.Equal(activity, ActivityModelMapper.MapToDetailModel(activityFromDb));
    }

    [Fact]
    public async Task SeededActivity_InsertOrUpdate_ActivityUpdated()
    {
        //Arrange
        var activity = new ActivityDetailModel()
        {
            Id = ActivitySeeds.Testing.Id,
            Name = @"nevim",
            Start = ActivitySeeds.Testing.Start,
            End = ActivitySeeds.Testing.End,
            Type = ActivitySeeds.Testing.Type,
            Description = ActivitySeeds.Testing.Description,
        };
        activity.End = new DateTime(2023, 4, 10);
        activity.Description += "updated";

        //Act
        await _activityFacadeSUT.SaveAsync(activity);

        //Assert
        await using var dbxAssert = await DbContextFactory.CreateDbContextAsync();
        var activityFromDb = await dbxAssert.Activities.SingleAsync(i => i.Id == activity.Id);
        DeepAssert.Equal(activity, ActivityModelMapper.MapToDetailModel(activityFromDb));
    }
}
