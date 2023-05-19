// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace Tracker.BL.Models;

public record ProjectUserListModel : ModelBase
{
    public required Guid ProjectId { get; set; }
    public required string ProjectName { get; set; }

    public static ProjectUserListModel Empty => new()
    {
        Id = Guid.NewGuid(),
        ProjectId = Guid.Empty,
        ProjectName = string.Empty
    };
}
