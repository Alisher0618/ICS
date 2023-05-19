// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.DAL.Entities;

namespace Tracker.DAL.Mappers;

public class ActivityProjectEntityMapper : IEntityMapper<ActivityProjectEntity>
{
    public void MapToExistingEntity(ActivityProjectEntity existingEntity, ActivityProjectEntity newEntity)
    {
        existingEntity.ProjectId = newEntity.ProjectId;
        existingEntity.ActivityId = newEntity.ActivityId;
    }
}
