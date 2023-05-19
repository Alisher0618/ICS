// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Collections.ObjectModel;

namespace Tracker.BL.Models;

public record ProjectDetailModel : ModelBase
{
    public required string Name { get; set; }
    public ObservableCollection<ActivityListModel> Activities { get; init; } = new();
    public ObservableCollection<UserListModel> Users { get; init; } = new();

    public static ProjectDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
    };
}
