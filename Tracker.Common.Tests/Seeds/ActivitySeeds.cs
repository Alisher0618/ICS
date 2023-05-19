// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.Common.Enums;
using Tracker.DAL.Entities;

namespace Tracker.Common.Tests.Seeds;
public static class ActivitySeeds
{
    public static readonly ActivityEntity EmptyActivity = new()
    {
        Id = default,
        Name = string.Empty,
        Start = default,
        End = default,
        Type = default,
        Description = default
    };

    public static readonly ActivityEntity Programming = new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        Start = default,
        End = default,
        Type = ActivityType.Programming,
        Description = default
    };

    public static readonly ActivityEntity Testing = new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        Start = default,
        End = default,
        Type = ActivityType.Testing,
        Description = default
    };

    public static readonly ActivityEntity TestingUpdate = Testing with { Id = Guid.Parse("143332B9-080E-4953-AEA5-BEF64679B052") };
    public static readonly ActivityEntity TestingDelete = Testing with { Id = Guid.Parse("274D0CC9-A948-4818-AADB-A8B4C0506619") };


    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ActivityEntity>().HasData(
            Programming,
            Testing,
            TestingDelete,
            TestingUpdate
            );
}
