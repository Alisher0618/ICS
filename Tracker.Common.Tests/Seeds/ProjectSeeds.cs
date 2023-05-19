// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.DAL.Entities;

namespace Tracker.Common.Tests.Seeds;
public static class ProjectSeeds
{
    public static readonly ProjectEntity EmptyProject = new()
    {
        Id = default,
        Name = default!,
        Activities = new List<ActivityProjectEntity>(),
        Users = new List<ProjectUserEntity>()
    };

    public static readonly ProjectEntity Project0 = new()
    {
        Id = Guid.NewGuid(),
        Name = "Project0",
        Activities = new List<ActivityProjectEntity>(),
        Users = new List<ProjectUserEntity>()
    };

    public static readonly ProjectEntity ProjectEntityUpdate = Project0 with { Id = Guid.Parse("0953F3CE-7B1A-48C1-9796-D2BAC7F67868"), Activities = new List<ActivityProjectEntity>() };
    public static readonly ProjectEntity ProjectEntityDelete = Project0 with { Id = Guid.Parse("5DCA4CEA-B8A8-4C86-A0B3-FFB78FBA1A09"), Activities = new List<ActivityProjectEntity>() };

    public static readonly ProjectEntity Project1 = new()
    {
        Id = Guid.NewGuid(),
        Name = "Project1",
        Activities = new List<ActivityProjectEntity>(),
        Users = new List<ProjectUserEntity>()
    };

    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ProjectEntity>().HasData(
            Project0,
            Project1,
            ProjectEntityUpdate,
            ProjectEntityDelete
            );
}
