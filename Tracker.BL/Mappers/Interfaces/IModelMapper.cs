// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Tracker.BL.Models;
using Tracker.DAL.Entities;

namespace Tracker.BL.Mappers.Interfaces
{
    // Explicitly stating mapper for only db entities and model.
    public interface IModelMapper<TEntity, out TListModel, TDetailModel>
        where TEntity : IEntity
        where TListModel : IModel
        where TDetailModel : IModel
    {
        TListModel MapToListModel(TEntity? entity);

        IEnumerable<TListModel> MapToListModel(IEnumerable<TEntity> entities)
            => entities.Select(MapToListModel);

        TDetailModel MapToDetailModel(TEntity entity);
        TEntity MapToEntity(TDetailModel model);
    }
}
