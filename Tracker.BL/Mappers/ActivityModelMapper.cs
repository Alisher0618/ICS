// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.BL.Mappers.Interfaces;
using Tracker.BL.Models;
using Tracker.DAL.Entities;

namespace Tracker.BL.Mappers;

public class ActivityModelMapper : ModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>,
    IActivityModelMapper
{
    public override ActivityListModel MapToListModel(ActivityEntity? entity) => entity is null
        ? ActivityListModel.Empty
        : new ActivityListModel { Id = entity.Id, Name = entity.Name, Start = entity.Start, End = entity.End };

    public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity) => entity is null
        ? ActivityDetailModel.Empty
        : new ActivityDetailModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Start = entity.Start,
            End = entity.End,
            Type = entity.Type,
            Description = entity.Description
        };

    public override ActivityEntity MapToEntity(ActivityDetailModel model) => new()
    {
        Id = model.Id,
        Name = model.Name,
        Start = model.Start,
        End = model.End,
        Type = model.Type,
        Description = model.Description
    };
}
