// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.Common.Tests;
using Tracker.DAL.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Tracker.DAL.Tests;
public class DbContextProjectUserTests : DbContextTestsBase
{
    public DbContextProjectUserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_ProjectUser_Persisted()
    {
        //Arrange
        ProjectUserEntity entity = new()
        {
            Id = Guid.Parse("D5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
            ProjectId = Guid.Parse("0953F3CE-7B1A-48C1-9796-D2BAC7F67868"),
            UserId = Guid.Parse("143332B9-080E-4953-AEA5-BEF64679B052"),
        };

        //Act
        TrackerDbContextSUT.ProjectsUserEntities.Add(entity);
        await TrackerDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        ProjectUserEntity actualEntities = await dbx.ProjectsUserEntities.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntities);
    }
}
