// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.DAL.Entities;

namespace Tracker.DAL.Mappers;

public class ProjectUserEntityMapper : IEntityMapper<ProjectUserEntity>
{
    public void MapToExistingEntity(ProjectUserEntity existingEntity, ProjectUserEntity newEntity)
    {
        existingEntity.ProjectId = newEntity.ProjectId;
        existingEntity.UserId = newEntity.UserId;
    }
}
