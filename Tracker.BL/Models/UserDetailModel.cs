// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Collections.ObjectModel;

namespace Tracker.BL.Models;

public record UserDetailModel : ModelBase
{
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? ImgUrl { get; set; }

    public ObservableCollection<ActivityListModel> Activities { get; init; } = new();

    public static UserDetailModel Empty => new()
    {
        Id = Guid.NewGuid(),
        Name = string.Empty,
        Surname = string.Empty,
        ImgUrl = string.Empty,
    };
}
