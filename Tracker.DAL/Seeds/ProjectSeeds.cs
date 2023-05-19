// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.DAL.Entities;

namespace Tracker.DAL.Seeds;
public static class ProjectSeeds
{
    public static readonly ProjectEntity Project0 = new()
    {
        Id = default,
        Name = "Project0",
        Activities = null,
        Users = null
    };

    public static readonly ProjectEntity Project1 = new()
    {
        Id = default,
        Name = "Project1",
        Activities = null,
        Users = null
    };

    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ProjectEntity>().HasData(Project0, Project1);
}
