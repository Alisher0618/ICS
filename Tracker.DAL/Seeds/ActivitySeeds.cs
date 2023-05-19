// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.Common.Enums;
using Tracker.DAL.Entities;

namespace Tracker.DAL.Seeds;
public static class ActivitySeeds
{
    public static readonly ActivityEntity Programming = new()
    {
        Id = default,
        Name = string.Empty,
        Start = default,
        End = default,
        Type = ActivityType.Programming
    };

    public static readonly ActivityEntity Testing = new()
    {
        Id = default,
        Name = string.Empty,
        Start = default,
        End = default,
        Type = ActivityType.Testing
    };

    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ActivityEntity>().HasData(Programming, Testing);
}
