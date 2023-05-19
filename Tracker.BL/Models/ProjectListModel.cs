// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace Tracker.BL.Models;

public record ProjectListModel : ModelBase
{
    public required string Name { get; set; }

    public static ProjectListModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
    };
}
