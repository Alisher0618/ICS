// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.Common.Tests;
using Tracker.DAL.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Tracker.DAL.Tests;
public class DbContextActivityProjectTests : DbContextTestsBase
{
    public DbContextActivityProjectTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task AddNew_ActivityProject_Persisted()
    {
        //Arrange
        ActivityProjectEntity entity = new()
        {
            Id = Guid.Parse("C5DE45D7-64A0-4E8D-AC7F-BF5CFDFB0EFC"),
            ProjectId = Guid.Parse("0953F3CE-7B1A-48C1-9796-D2BAC7F67868"),
            ActivityId = Guid.Parse("143332B9-080E-4953-AEA5-BEF64679B052"),
        };

        //Act
        TrackerDbContextSUT.ActivityProjectEntities.Add(entity);
        await TrackerDbContextSUT.SaveChangesAsync();

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        ActivityProjectEntity actualEntities = await dbx.ActivityProjectEntities.SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntities);
    }
}
