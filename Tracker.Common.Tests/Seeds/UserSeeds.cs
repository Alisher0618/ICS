// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.DAL.Entities;

namespace Tracker.Common.Tests.Seeds;
public static class UserSeeds
{
    public static readonly UserEntity EmptyUser = new()
    {
        Id = default,
        Name = default!,
        Surname = default!,
        ImgUrl = default,
        Activities = default!,
        Projects = default!
    };


    public static readonly UserEntity User0 = new()
    {
        Id = Guid.NewGuid(),
        Name = "Antonín",
        Surname = "Zeman",
        ImgUrl = null,
        Activities = new List<ActivityEntity>(),
        Projects = new List<ProjectUserEntity>()
    };


    //To ensure that no tests reuse these clones for non-idempotent operations
    public static readonly UserEntity UserUpdate = User0 with { Id = Guid.Parse("143332B9-080E-4953-AEA5-BEF64679B052") };
    public static readonly UserEntity UserDelete = User0 with { Id = Guid.Parse("274D0CC9-A948-4818-AADB-A8B4C0506619") };


    public static readonly UserEntity User1 = new()
    {
        Id = Guid.NewGuid(),
        Name = "František",
        Surname = "Fiala",
        ImgUrl = null,
        Activities = new List<ActivityEntity>(),
        Projects = new List<ProjectUserEntity>()
    };


    public static readonly UserEntity UserEntity = new()
    {
        Id = Guid.NewGuid(),
        Name = "Jozef",
        Surname = "Vajda",
        ImgUrl = null,
        Activities = new List<ActivityEntity>(),
        Projects = new List<ProjectUserEntity>()
    };


    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<UserEntity>().HasData(
            UserEntity,
            UserUpdate,
            UserDelete,
            User0,
            User1
            );
}
