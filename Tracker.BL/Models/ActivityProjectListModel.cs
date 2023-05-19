// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace Tracker.BL.Models;

public record ActivityProjectListModel : ModelBase
{
    public required Guid ActivityId { get; set; }
    public required string ActivityName { get; set; }

    public static ActivityProjectListModel Empty => new()
    {
        Id = Guid.NewGuid(),
        ActivityId = Guid.Empty,
        ActivityName = string.Empty,
    };
}
