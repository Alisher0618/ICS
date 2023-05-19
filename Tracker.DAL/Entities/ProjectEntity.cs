// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace Tracker.DAL.Entities;

public record ProjectEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public ICollection<ActivityProjectEntity> Activities { get; init; } = new List<ActivityProjectEntity>();
    public ICollection<ProjectUserEntity> Users { get; init; } = new List<ProjectUserEntity>();
}
