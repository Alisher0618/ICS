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

/// <summary>
/// Tests shows an example of DbContext usage when querying strong entity with no navigation properties.
/// Entity has no relations, holds no foreign keys.
/// </summary>
public class DbContextUserTests : DbContextTestsBase
{
    public DbContextUserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_User_Persisted()
    {
        //Arrange
        UserEntity entity = new()
        {
            Id = Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
            Name = "Jozef",
            Surname = "Vajda",
            ImgUrl = "https://images.emojiterra.com/twitter/v13.1/512px/1f913.png"
        };

        //Act
        TrackerDbContextSUT.Users.Add(entity);
        await TrackerDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        UserEntity actualEntities = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntities);
    }

    [Fact]
    public async Task GetAll_Users_ContainsSeededUser0()
    {
        //Act
        var entities = await TrackerDbContextSUT.Users.ToArrayAsync();

        //Assert
        Assert.Contains(entities, e => e.Id.Equals(UserSeeds.User0.Id));
    }

    [Fact]
    public void GetAll_Users_FindUser0()
    {
        //Act
        var entity = TrackerDbContextSUT.Users
            .FirstOrDefaultAsync(i => UserSeeds.User0.Id.Equals(i.Id));

        Assert.NotNull(entity);
    }

    [Fact]
    public async Task GetById_User_UserEntityRetrieved()
    {
        //Act
        UserEntity entity = await TrackerDbContextSUT.Users.SingleAsync(i => i.Id == UserSeeds.UserEntity.Id);

        //Assert
        DeepAssert.Equal(UserSeeds.UserEntity, entity);
    }

    [Fact]
    public async Task Update_User_Persisted()
    {
        //Arrange
        UserEntity baseEntity = UserSeeds.UserUpdate;
        UserEntity entity =
            baseEntity with
            {
                Name = baseEntity + "Updated",
                Surname = baseEntity + "Updated",
            };

        //Act
        TrackerDbContextSUT.Users.Update(entity);
        await TrackerDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntities = await dbx.Users.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntities);
    }

    [Fact]
    public async Task DeleteById_User_Deleted()
    {
        //Arrange
        var baseEntity = UserSeeds.UserDelete;

        //Act
        TrackerDbContextSUT.Remove(
            TrackerDbContextSUT.Users.Single(i => i.Id == baseEntity.Id));
        await TrackerDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await TrackerDbContextSUT.Users.AnyAsync(i => i.Id == baseEntity.Id));
    }

}
