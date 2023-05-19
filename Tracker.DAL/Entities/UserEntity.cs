// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

namespace Tracker.DAL.Entities;

public record UserEntity : IEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? ImgUrl { get; set; }
    public ICollection<ActivityEntity> Activities { get; init; } = new List<ActivityEntity>();
    public ICollection<ProjectUserEntity> Projects { get; init; } = new List<ProjectUserEntity>();
}
