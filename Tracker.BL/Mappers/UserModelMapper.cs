// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using System.Collections.ObjectModel;
using Tracker.BL.Mappers.Interfaces;
using Tracker.BL.Models;
using Tracker.DAL.Entities;

namespace Tracker.BL.Mappers;

public class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel>,
    IUserModelMapper
{
    private readonly IActivityModelMapper _activityModelMapper;

    public UserModelMapper(IActivityModelMapper activityModelMapper)
    {
        _activityModelMapper = activityModelMapper;
    }

    public override UserListModel MapToListModel(UserEntity? entity) => entity is null
        ? UserListModel.Empty
        : new UserListModel { Id = entity.Id, ImgUrl = entity.ImgUrl, Name = entity.Name, Surname = entity.Surname };

    public override UserDetailModel MapToDetailModel(UserEntity? entity) => entity is null
        ? UserDetailModel.Empty
        : new UserDetailModel
        {
            Id = entity.Id,
            Name = entity.Name,
            Surname = entity.Surname,
            Activities = new ObservableCollection<ActivityListModel>(_activityModelMapper.MapToListModel(
                entity.Activities))
        };

    public override UserEntity MapToEntity(UserDetailModel model) => new()
    {
        Id = model.Id,
        Name = model.Name,
        Surname = model.Surname,
        ImgUrl = model.ImgUrl
    };
}
