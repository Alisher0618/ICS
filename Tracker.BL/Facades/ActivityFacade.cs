// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using Tracker.BL.Facades.Interfaces;
using Tracker.BL.Mappers.Interfaces;
using Tracker.BL.Models;
using Tracker.DAL.Entities;
using Tracker.DAL.Mappers;
using Tracker.DAL.UnitOfWork;

namespace Tracker.BL.Facades;

public class ActivityFacade : FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>, IActivityFacade
{
    public ActivityFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IActivityModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    { }

    private async Task<UserEntity?> GetUserEntity(Guid userId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        UserEntity? entity = await uow
            .GetRepository<UserEntity, UserEntityMapper>()
            .Get()
            .FirstOrDefaultAsync(e => e.Id == userId);
        return entity;
    }

    public virtual async Task<IEnumerable<ActivityListModel>> GetActivityListAsync(Guid userId)
    {
        UserEntity? entity = await GetUserEntity(userId);

        return entity is null
            ? new ObservableCollection<ActivityListModel>()
            : ModelMapper.MapToListModel(entity.Activities);
    }

    public virtual async Task<IEnumerable<ActivityListModel>> GetActivityListAsync(Guid userId, DateTime? from, DateTime? to)
    {
        if (from is null) from = DateTime.MinValue;
        if (to is null) to = DateTime.MaxValue;

        UserEntity? entity = await GetUserEntity(userId);

        if (entity is null) { return new ObservableCollection<ActivityListModel>(); }

        IEnumerable<ActivityEntity> Activities = entity.Activities.Where(e => (e.Start >= from) && (e.End <= to))
                .OrderBy(e => e.Start);

        return ModelMapper.MapToListModel(Activities);
    }
}
