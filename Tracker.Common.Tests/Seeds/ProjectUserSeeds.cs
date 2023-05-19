// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//
using Tracker.DAL.Entities;

namespace Tracker.Common.Tests.Seeds;

public static class ProjectUserSeeds
{
    public static readonly ProjectUserEntity EmptyProjectUser = new()
    {
        Id = default,
        Project = default,
        ProjectId = default,
        User = default,
        UserId = default
    };
}
