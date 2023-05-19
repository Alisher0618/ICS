// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Microsoft.EntityFrameworkCore;
using Tracker.BL.Facades.Interfaces;
using Tracker.BL.Mappers.Interfaces;
using Tracker.BL.Models;
using Tracker.Common;
using Tracker.DAL.Entities;
using Tracker.DAL.Mappers;
using Tracker.DAL.UnitOfWork;

namespace Tracker.BL.Facades;

public class UserFacade : FacadeBase<UserEntity, UserListModel, UserDetailModel, UserEntityMapper>, IUserFacade
{
    public UserFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }
    protected override string IncludesNavigationPathDetail =>
        $"{nameof(UserEntity.Projects)}.{nameof(ProjectUserEntity.Project)}";

    //add getAsync account for 3 activites
    public override async Task<UserDetailModel?> GetAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();

        IQueryable<UserEntity> query = uow.GetRepository<UserEntity, UserEntityMapper>().Get();

        if (string.IsNullOrWhiteSpace(IncludesNavigationPathDetail) is false)
        {
            query = query.Include(IncludesNavigationPathDetail);
        }

        UserEntity? entity = await query.SingleOrDefaultAsync(e => e.Id == id);

        UserDetailModel? model = entity is null ? null : ModelMapper.MapToDetailModel(entity);
        if (model != null)
        {
            IEnumerable<ActivityListModel> Activities = model.Activities.Where(e => e.Start > DateTime.Now)
                    .OrderBy(e => e.Start).Take(Constants.MaxPrioritizedUserActivities);
            model.Activities.Clear();
            foreach (ActivityListModel activities in Activities)
            {
                model.Activities.Add(activities);
            }
        }

        return model;
    }
}
