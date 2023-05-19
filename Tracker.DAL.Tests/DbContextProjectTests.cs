// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.Common.Tests;
using Tracker.Common.Tests.Seeds;
using Tracker.DAL.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Tracker.DAL.Tests;
public class DbContextProjectTests : DbContextTestsBase
{
    public DbContextProjectTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_Project_Persisted()
    {
        //Arrange
        ProjectEntity entity = new()
        {
            Id = Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
            Name = "Epic Builder",
            Activities = new List<ActivityProjectEntity>()
        };

        //Act
        TrackerDbContextSUT.Projects.Add(entity);
        await TrackerDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        ProjectEntity actualEntities = await dbx.Projects.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntities);
    }

    [Fact]
    public async Task GetById_Project()
    {
        //Act
        var entity = await TrackerDbContextSUT.Projects
            .SingleAsync(i => i.Id == ProjectSeeds.Project0.Id);

        //Assert
        DeepAssert.Equal(ProjectSeeds.Project0 with { Activities = Array.Empty<ActivityProjectEntity>() }, entity);
    }

    [Fact]
    public async Task Update_Project_Persisted()
    {
        //Arrange
        var baseEntity = ProjectSeeds.ProjectEntityUpdate;
        var entity =
            baseEntity with
            {
                Name = baseEntity.Name + "Updated",
            };

        //Act
        TrackerDbContextSUT.Projects.Update(entity);
        await TrackerDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Projects.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task DeleteById_Project_Deleted()
    {
        //Arrange
        var baseEntity = ProjectSeeds.ProjectEntityDelete;

        //Act
        TrackerDbContextSUT.Remove(
            TrackerDbContextSUT.Projects.Single(i => i.Id == baseEntity.Id));
        await TrackerDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await TrackerDbContextSUT.Projects.AnyAsync(i => i.Id == baseEntity.Id));
    }
}
