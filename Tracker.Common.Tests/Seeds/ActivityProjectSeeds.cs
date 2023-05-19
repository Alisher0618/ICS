// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//
using Tracker.DAL.Entities;
namespace Tracker.Common.Tests.Seeds;

public static class ActivityProjectSeeds
{
    public static readonly ActivityProjectEntity EmptyActivityProject = new()
    {
        Id = default,
        Project = default,
        ProjectId = default,
        Activity = default,
        ActivityId = default
    };
}
