// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.Common;
using Tracker.Common.Enums;

namespace Tracker.BL.Models;

public record ActivityDetailModel : ModelBase
{
    public required string Name { get; set; }
    public required DateTime Start { get; set; }
    public required DateTime End { get; set; }
    public required ActivityType Type { get; set; }
    public string? Description { get; set; }

    public static ActivityDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        Start = DateTime.Now,
        End = DateTime.Now.AddMinutes(Constants.MinActivityLenghtInMinutes),
        Type = ActivityType.Default,
        Description = string.Empty,
    };
}
