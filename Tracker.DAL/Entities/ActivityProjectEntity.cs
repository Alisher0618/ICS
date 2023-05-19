// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace Tracker.DAL.Entities;

public record ActivityProjectEntity : IEntity
{
    public required Guid ProjectId { get; set; }
    public ProjectEntity? Project { get; init; }
    public required Guid ActivityId { get; set; }
    public ActivityEntity? Activity { get; init; }
    public required Guid Id { get; set; }
}

