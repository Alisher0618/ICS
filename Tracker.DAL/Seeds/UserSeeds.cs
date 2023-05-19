// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.DAL.Entities;

namespace Tracker.DAL.Seeds;
public static class UserSeeds
{
    public static readonly UserEntity User0 = new()
    {
        Id = default,
        Name = "Antonín",
        Surname = "Zeman",
        ImgUrl = null,
        Activities = null,
        Projects = null
    };


    public static readonly UserEntity User1 = new()
    {
        Id = default,
        Name = "František",
        Surname = "Fiala",
        ImgUrl = null,
        Activities = null,
        Projects = null
    };


    public static void Seed(this ModelBuilder modelBuilder) =>
            modelBuilder.Entity<UserEntity>().HasData(User0, User1);
}
